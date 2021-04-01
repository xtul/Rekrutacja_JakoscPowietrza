using EmtelRekrutacja.Entities;
using Flurl.Http;
using LazyCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmtelRekrutacja.Controllers {
	[Route("api")]
	[ApiController]
	public class AirQualityApiController : ControllerBase {
		public IAppCache _cache;

		public AirQualityApiController(IAppCache cache) {
			_cache = cache;
		}

		[HttpGet]
		[Route("summary/{stationId}")]
		public async Task<Summary> GetSummaryAsync(int stationId) {
			var result = await _cache.GetOrAdd($"summary-{stationId}",
				() => GenerateSummary(stationId),
				TimeSpan.FromMinutes(10));

			return result;
		}

		private async Task<Summary> GenerateSummary(int stationId) {
			// get all sensors and their newest values
			List<string> sensorValues = new();
			Sensor[] availableSensors = await GetArrayAsync<Sensor>("http://api.gios.gov.pl/pjp-api/rest/station/sensors", stationId);

			foreach (var sensor in availableSensors) {
				var sensorResults = await GetSingleAsync<SensorValue>("http://api.gios.gov.pl/pjp-api/rest/data/getData", sensor.Id);

				try {
					sensorValues.Add($"{sensor.Param.ParamName}:{sensorResults.Values.FirstOrDefault().Amount}");
				} catch (NullReferenceException) { }
			}

			// get measurements and format it to "type: verbose-level"
			Measurements measurements = await GetSingleAsync<Measurements>("http://api.gios.gov.pl/pjp-api/rest/aqindex/getIndex", stationId);

			var noData = "Brak danych";
			var measurementList = new List<string>() {
				$"Ogólne:{measurements.StIndexLevel?.IndexLevelName ?? noData}",
				$"Dwutlenek siarki (SO2):{measurements.So2IndexLevel?.IndexLevelName ?? noData}",
				$"Dwutlenek azotu (NO2):{measurements.No2IndexLevel?.IndexLevelName ?? noData}",
				$"Pył PM10:{measurements.Pm10IndexLevel?.IndexLevelName ?? noData}",
				$"Pył PM2,5:{measurements.Pm25IndexLevel?.IndexLevelName ?? noData}",
				$"Tlenek węgla (CO):{measurements.CoIndexLevel?.IndexLevelName ?? noData}",
				$"Benzen (C6H6):{measurements.C6h6IndexLevel?.IndexLevelName ?? noData}",
				$"Ozon (O3):{measurements.O3IndexLevel?.IndexLevelName ?? noData}"
			};

			return new Summary() {
				SensorValues = sensorValues,
				Measurements = measurementList
			};
		}

		private async Task<T> GetSingleAsync<T>(string url, int id) {
			if (id == 0) {
				return default;
			}

			try {
				return await $"{url}/{id}".GetJsonAsync<T>();
			} catch (FlurlHttpException) {
				return default;
			}
		}

		private async Task<T[]> GetArrayAsync<T>(string url, int id) {
			if (id == 0) {
				return default;
			}

			try {
				return await $"{url}/{id}".GetJsonAsync<T[]>();
			} catch (FlurlHttpException) {
				return default;
			}
		}
	}
}

using EmtelRekrutacja.Entities;
using System.Collections.Generic;
using Flurl.Http;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace EmtelRekrutacja {
	public class LocationProvider {
		private List<Location> _locations = new();
		public ILogger<LocationProvider> _logger;

		public LocationProvider(ILogger<LocationProvider> logger) {
			_logger = logger;
		}

		/// <summary>
		/// Downloads weather locations from API. Gets only locations around Poznań, as required.
		/// </summary>
		/// <returns></returns>
		public async Task DownloadLocations() {
			var api = await "http://api.gios.gov.pl/pjp-api/rest/station/findAll".GetJsonAsync<Location[]>();
			_locations = api.Where(x => x.City.Name == "Poznań").ToList();
		}

		public async Task<List<Location>> GetLocationsAsync() {
			if (_locations.Count < 1) {
				await DownloadLocations();
			}

			return _locations;
		}
	}
}
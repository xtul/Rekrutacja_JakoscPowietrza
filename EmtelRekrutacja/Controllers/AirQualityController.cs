using EmtelRekrutacja.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmtelRekrutacja.Controllers {
	public class AirQualityController : Controller {
		public LocationProvider LocationProvider { get; set; }

		public AirQualityController(LocationProvider locationProvider) {
			LocationProvider = locationProvider;
		}

		public async Task<IActionResult> IndexAsync() {
			ViewData["Title"] = "Przegląd";
			ViewData["Locations"] = await LocationProvider.GetLocationsAsync();

			return View();
		}
	}
}

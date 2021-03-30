using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmtelRekrutacja.Entities {
	public class Location {
		public int Id { get; set; }
		public string StationName { get; set; }
		public string GegrLat { get; set; }
		public string GegrLon { get; set; }
		public City City { get; set; }
		public string AddressStreet { get; set; }
	}
	public class Commune {
		public string CommuneName { get; set; }
		public string DistrictName { get; set; }
		public string ProvinceName { get; set; }
	}

	public class City {
		public int Id { get; set; }
		public string Name { get; set; }
		public Commune Commune { get; set; }
	}

}

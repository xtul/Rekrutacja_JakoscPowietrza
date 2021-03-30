using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmtelRekrutacja.Entities {
	public class Value {
		public string Date { get; set; }
		[JsonPropertyName("value")]
		public double Amount { get; set; }
	}

	public class SensorValue {
		public string Key { get; set; }
		[JsonPropertyName("values")]
		public List<Value> Values { get; set; }
	}
}

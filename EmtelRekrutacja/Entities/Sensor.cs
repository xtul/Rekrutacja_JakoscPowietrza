using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmtelRekrutacja.Entities {
    public class Param {
        public string ParamName { get; set; }
        public string ParamFormula { get; set; }
        public string ParamCode { get; set; }
        public int IdParam { get; set; }
    }

    public class Sensor {
        public int Id { get; set; }
        public int StationId { get; set; }
        public Param Param { get; set; }
    }
}

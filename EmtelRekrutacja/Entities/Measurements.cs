using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmtelRekrutacja.Entities {
    public class IndexLevel {
        public int Id { get; set; }
        public string IndexLevelName { get; set; }
    }

    public class Measurements {
        public int Id { get; set; }
        public string StCalcDate { get; set; }
        public IndexLevel StIndexLevel { get; set; }
        public string StSourceDataDate { get; set; }
        public string So2CalcDate { get; set; }
        public IndexLevel So2IndexLevel { get; set; }
        public string So2SourceDataDate { get; set; }
        public string No2CalcDate { get; set; }
        public IndexLevel No2IndexLevel { get; set; }
        public string No2SourceDataDate { get; set; }
        public string CoCalcDate { get; set; }
        public IndexLevel CoIndexLevel { get; set; }
        public string CoSourceDataDate { get; set; }
        public string Pm10CalcDate { get; set; }
        public IndexLevel Pm10IndexLevel { get; set; }
        public string Pm10SourceDataDate { get; set; }
        public string Pm25CalcDate { get; set; }
        public IndexLevel Pm25IndexLevel { get; set; }
        public string Pm25SourceDataDate { get; set; }
        public string O3CalcDate { get; set; }
        public IndexLevel O3IndexLevel { get; set; }
        public string O3SourceDataDate { get; set; }
        public string C6h6CalcDate { get; set; }
        public IndexLevel C6h6IndexLevel { get; set; }
        public string C6h6SourceDataDate { get; set; }
    }


}

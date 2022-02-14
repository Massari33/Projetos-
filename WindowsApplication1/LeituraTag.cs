using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViaondaRFID_MID10S_DEMO {
    class LeituraTag {
        public string tipo { get; set; }
        public string ant { get; set; }
        public string tag { get; set; }
        public int leituras { get; set; }
        public string RSSI { get; set; }
    }
}

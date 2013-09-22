using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoCompleteModel
{
    [Serializable]
    public class AutoCompleteItem
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}

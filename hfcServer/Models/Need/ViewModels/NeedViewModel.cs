using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hfcServer.Models
{
    public class NeedViewModel
    {
        public string NeedID { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public NeedViewModel(Need n)
        {
            NeedID = n.Id.ToString();
            Name = n.Name;
            Unit = n.Units;
        }
    }
}
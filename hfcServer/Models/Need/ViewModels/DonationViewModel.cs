using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hfcServer.Models
{
    public class DonationViewModel
    {
        public String label { get; set; }
        public int y { get; set; }
        public DonationViewModel(Need n)
        {
            var temp = n.Donations;
            label = n.Name + " " + n.Units;
            y = temp.Sum(d => d.Amount);
        }
        public string ToJson()
        {
            return "{\nlabel: " + label + ",\ny: " + y.ToString() + "\n}";
        }
    }
}
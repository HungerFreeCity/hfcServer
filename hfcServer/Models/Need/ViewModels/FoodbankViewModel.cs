using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace hfcServer.Models
{
    public class FoodbankViewModel
    {
        public string FoodBankID { get; set; }
        public string Location { get; set; }
        public string FoodBankName { get; set; }
        public string Website { get; set; }
        public ObservableCollection<NeedViewModel> NeedsList;
        public FoodbankViewModel(FoodBank fb, ObservableCollection<NeedViewModel> needs)
        {
            FoodBankID = fb.Id.ToString();
            Location = fb.Address;
            FoodBankName = fb.Name;
            if (fb.WebsiteURL == null)
                Website = "";
            else
                Website = fb.WebsiteURL;
            NeedsList = needs;
        }
    }
}
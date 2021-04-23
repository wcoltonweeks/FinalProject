using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.UI.MVC.Models
{
    //[JsonObject(MemberSerialization.OptIn)]
    public class APIDataModel
    {

        public string ID { get; set; }        public int Rank { get; set; }        public string Country { get; set; }        public string Continent { get; set; }        public string TwoLetterSymbol { get; set; }        public string ThreeLetterSymbol { get; set; }        public decimal Infection_Risk { get; set; }        public decimal Case_Fatality_Rate { get; set; }        public decimal Test_Percentage { get; set; }        public decimal Recovery_Proporation { get; set; }        public int TotalCases { get; set; }        public int NewCases { get; set; }        public int TotalDeaths { get; set; }        public int NewDeaths { get; set; }        public int TotalRecovered { get; set; }        public int NewRecovered { get; set; }        public int TotalTests { get; set; }        public int Population { get; set; }        public decimal One_Case_Every_X_Ppl { get; set; }        public decimal One_Death_Every_X_Ppl { get; set; }        public decimal One_Test_Every_X_Ppl { get; set; }
        public decimal Deaths_1M_Pop { get; set; }        public int Serious_Critical { get; set; }        public decimal Tests_1M_Pop { get; set; }        public decimal TotalCases_1M_Pop { get; set; }

    }

    public class APIList
    {
        public List<APIDataModel> ApiList { get; set; }
    }
}
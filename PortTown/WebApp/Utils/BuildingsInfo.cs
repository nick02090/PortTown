using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Resources;

namespace WebApp.Utils
{
    public class BuildingsInfo
    {

        public static readonly string FARM_INFO = "Ovo je farma. Tu ima krmadi koja ce se poslje klat.";
        public static readonly string WORKSHOP_INFO = "Ovo je radionica.";
        public static readonly string GOLD_MINE_INFO = "Rudnik zlata";
        public static readonly string SILO_INFO = "Ovo je šiljilo za olovke";
        public static readonly string WAREHOUSE_INFO = "Ovo je skladiste";
        public static readonly string QUARRY_INFO = "Ovo je kamenolom.";
        public static readonly string COAL_MINE = "abacabcabc";
        public static readonly string IRON_MINE = "Aj pogodi sta je ovo jbt";
        public static readonly string SAMWILL_INFO = "Nisam uspio nac bolje ime za pilanu, al mocno zvuci (reci 100 puta u sebi samwill)";
        public static readonly string PALACE_INFO = "Ovo je palaca. Ovde se organiziraju orgije";

        public static readonly Dictionary<string, string> NameToInfo = new Dictionary<string, string>
        {
            {"Farm", FARM_INFO },
            {"Workshop", WORKSHOP_INFO },
            {"Gold mine", GOLD_MINE_INFO },
            {"Silo", SILO_INFO },
            {"Warehouse", WAREHOUSE_INFO },
            {"Quarry", QUARRY_INFO },
            {"Coal mine", COAL_MINE },
            {"Iron mine", IRON_MINE },
            {"Sawmill", SAMWILL_INFO },
            {"Palace", PALACE_INFO }
        };

        public static readonly Dictionary<string, ResourceType> NameToResourceType = new Dictionary<string, ResourceType>
        {
            {"Farm", ResourceType.Food },
            {"Gold mine", ResourceType.Gold },
            {"Stone storage", ResourceType.Stone },
            {"Quarry", ResourceType.Stone },
            {"Coal mine", ResourceType.Coal },
            {"Iron mine", ResourceType.Iron},
            {"Sawmill", ResourceType.Wood },
        };
    }
}
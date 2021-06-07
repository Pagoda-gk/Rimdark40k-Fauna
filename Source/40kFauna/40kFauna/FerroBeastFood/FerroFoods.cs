using Verse;
using RimWorld;
using System.Collections.Generic;

namespace PGD_40kFauna
{


    public static class FerroFoods
    {
           
        // set this to the def of your ferrobeasts race
        public static ThingDef PGD_FerroBeast = DefDatabase<ThingDef>.GetNamed("PGD_FerroBeast", true);
        public static JobDef Ferro_Eat = DefDatabase<JobDef>.GetNamed("PGD_IngestMetallic", true);
        public static List<ThingDef> foods;


        public static List<ThingDef> Foods
        {

            get
            {
                if (foods == null)
                {
                    foods = DefDatabase<ThingDef>.AllDefsListForReading.FindAll(x => x.stuffProps?.categories != null && x.stuffProps.categories.Contains(StuffCategoryDefOf.Metallic));
                }
                return foods;
            }
        }


            public static float NutritionForMetallic(Thing thing)
            {
                return thing.def.BaseMass * thing.def.BaseMarketValue;
            }

        
    }
   
}
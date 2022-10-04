using Verse;
using RimWorld;
using System.Collections.Generic;
using System;

namespace AdeptusMechanicus
{
    public static class FerroFoods
    {
        public static List<ThingDef> foods;
        public static float NutritionForMetallic(Thing thing) => (float)Math.Max(thing.def.BaseMass / 2, 0.01);
        public static List<ThingDef> Foods
        {

            get
            {
                if (foods == null)
                {
                    foods = new List<ThingDef>();
                    foods = DefDatabase<ThingDef>.AllDefsListForReading.FindAll(x => x.stuffProps?.categories != null && x.stuffProps.categories.Contains(StuffCategoryDefOf.Metallic));
                }
                return foods;
            }
        }

    }
   
}
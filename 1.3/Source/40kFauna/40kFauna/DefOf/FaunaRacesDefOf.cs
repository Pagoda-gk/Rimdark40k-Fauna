using RimWorld;
using Verse;

namespace AdeptusMechanicus
{
    [DefOf]
    public static class FaunaRacesDefOf
	{
		static FaunaRacesDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(FaunaRacesDefOf));
		}
		public static ThingDef OG_Xenos_Grox;
		public static ThingDef OG_Xenos_Khymera;
		public static ThingDef OG_Xenos_FerroBeast;
		public static ThingDef OG_Xenos_XothicBloodLocustS;

	}
}

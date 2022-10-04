using RimWorld;

namespace AdeptusMechanicus
{
    [DefOf]
    public static class FaunaThoughtDefOf
	{
		static FaunaThoughtDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(FaunaThoughtDefOf));
		}
		public static ThoughtDef OG_ObservedWarpBeing;

	}
}

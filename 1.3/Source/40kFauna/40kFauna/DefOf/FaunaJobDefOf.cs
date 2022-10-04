using RimWorld;
using Verse;

namespace AdeptusMechanicus
{
    [DefOf]
    public static class FaunaJobDefOf
	{
		static FaunaJobDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(FaunaJobDefOf));
		}
		public static JobDef OG_IngestMetallic;

	}
}

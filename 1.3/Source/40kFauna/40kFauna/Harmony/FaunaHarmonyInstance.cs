using System.Reflection;
using HarmonyLib;
using Verse;

namespace AdeptusMechanicus
{
    [StaticConstructorOnStartup]
    public class FaunaHarmonyInstance : Mod
	{
		public FaunaHarmonyInstance(ModContentPack content) : base(content)
		{
			var harmony = new Harmony("com.pagoda.rimworld.mod.fauna");
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
	}

}


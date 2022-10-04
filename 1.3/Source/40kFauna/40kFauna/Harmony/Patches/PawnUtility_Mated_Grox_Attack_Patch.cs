using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AdeptusMechanicus
{
    [HarmonyPatch(typeof(PawnUtility), "Mated")]
    public static class PawnUtility_Mated_Grox_Attack_Patch
	{

		[HarmonyPostfix]
		public static void Postfix(Pawn male, Pawn female)
		{
			if (male.def == FaunaRacesDefOf.OG_Xenos_Grox) IsGrox(male);
			if (female.def == FaunaRacesDefOf.OG_Xenos_Grox) IsGrox(female);

		}
		public static void IsGrox(Pawn pawn)
		{
			if (pawn != null && Rand.Chance(0.05f))
			{

				string MessageGroxAttack = "{0} has become enraged and is making a territorial display.";

				String.Format(MessageGroxAttack, pawn.Name);

				pawn.mindState.mentalStateHandler.TryStartMentalState(DefDatabase<MentalStateDef>.GetNamed("Manhunter"), null, false, false, null, false);

			}
		}
	}

}


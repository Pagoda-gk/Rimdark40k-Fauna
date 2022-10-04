using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace AdeptusMechanicus
{
    //PawnUtility.Mated
    //Pawn_InteractionsTracker.TryInteractWith()

    [HarmonyPatch(typeof(Pawn_InteractionsTracker), "TryInteractWith")]
		public static class Pawn_InteractionsTracker_TryInteractWith_Grox_Attack_Patch
		{
			
			[HarmonyPostfix]
			public static void Postfix(Pawn_InteractionsTracker __instance, Pawn ___pawn, Pawn recipient, InteractionDef intDef, ref bool __result)
			{
						
			if (recipient.def == FaunaRacesDefOf.OG_Xenos_Grox && Rand.Chance(0.05f))
				{

				string MessageGroxAttack = "{0} has become enraged and is making a territorial display.";
								
					String.Format(MessageGroxAttack, recipient.Name);

				recipient.mindState.mentalStateHandler.TryStartMentalState(DefDatabase<MentalStateDef>.GetNamed("Manhunter"), null, false, false, null, false);

				}

			}
		}

}


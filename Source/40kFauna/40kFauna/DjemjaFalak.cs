using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using System.Reflection;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace PGD_40kFauna
{

	[HarmonyPatch(typeof(Toils_Interpersonal), "TryRecruit")]
	public static class Pawn_Toils_Interpersonal_DjemjaFalak_Recruit
	{


		[HarmonyPostfix]
		public static void Postfix(Toil __instance, ref Toil __result)
		{
			Log.Message("we're actually fixing the right thing here");
			Pawn pawn = __instance.actor;
			if (pawn != null)
			{
				Log.Message("target pawn was found");

				//check for djemja falak

				Hediff DjemjaFalak = pawn.health.hediffSet.GetFirstHediffOfDef(PGD_40kFaunaDefOf.PGD_DjemjaFalak);
				if (DjemjaFalak != null)
				{
					Log.Message("hediff was found");
					//then drop resistance by another 5 - 20

					if (pawn.guest != null && pawn.guest.resistance > 0f)
					{
						Log.Message("the resistance condition was met");
						float extraResistanceDrop = Rand.Range(5, 20);
						pawn.guest.resistance = Mathf.Max(0f, pawn.guest.resistance - extraResistanceDrop);
						DebugActionsUtility.DustPuffFrom(pawn);

						//then rand.chance for brain damage - based on ticks since install

						if (pawn.Dead)
						{
							return;
						}
						if (Rand.Value <= DjemjaFalak.ageTicks * 0.00005)
						{
							BodyPartRecord brain = pawn.health.hediffSet.GetBrain();
							if (brain == null)
							{
								return;
							}
							int num = Rand.RangeInclusive(1, 5);
							pawn.TakeDamage(new DamageInfo(DamageDefOf.Flame, (float)num, 0f, -1f, pawn, brain, null, DamageInfo.SourceCategory.ThingOrUnknown, null));
						}
					}

					
				}
				
			}

			return;
		}
	}
		
	
}

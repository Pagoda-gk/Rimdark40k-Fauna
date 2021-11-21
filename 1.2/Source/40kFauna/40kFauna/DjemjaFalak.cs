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

    [HarmonyPatch(typeof(InteractionWorker_RecruitAttempt), "Interacted")]
    public static class InteractionWorker_RecruitAttempt_Interacted_DjemjaFalak
    {
        [HarmonyPrefix]
        public static void Prefix(Pawn initiator, Pawn recipient)
        {
            Hediff DjemjaFalak = recipient.health.hediffSet.GetFirstHediffOfDef(PGD_40kFaunaDefOf.PGD_DjemjaFalak);
            if (DjemjaFalak != null)
            {
                //Log.Message("hediff was found");
                //then drop resistance by another 5 - 20

                if (recipient.guest != null && recipient.guest.resistance > 0f)
                {
                    //Log.Message("the resistance condition was met");
                    //float extraResistanceDrop = Rand.Range(5, 20);
                    recipient.guest.resistance = Mathf.Max(0f, recipient.guest.resistance - 5);
                    DebugActionsUtility.DustPuffFrom(recipient);

                    //then rand.chance for brain damage - based on ticks since install

                    if (recipient.Dead)
                    {
                        return;
                    }
                    if (Rand.Value <= DjemjaFalak.ageTicks * 0.000001)
                    {
                        BodyPartRecord brain = recipient.health.hediffSet.GetBrain();
                        if (brain == null)
                        {
                            return;
                        }
                        int num = Rand.RangeInclusive(1, 5);
                        recipient.TakeDamage(new DamageInfo(DamageDefOf.Burn, (float)num, 0f, -1f, recipient, brain, null, DamageInfo.SourceCategory.ThingOrUnknown, null));
                    }
                }


            }

        }
    }


}

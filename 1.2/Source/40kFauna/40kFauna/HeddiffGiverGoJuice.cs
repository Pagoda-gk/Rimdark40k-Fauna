using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using System.Text;
using System.Threading.Tasks;

namespace PGD_40kFauna
{
    public class HediffGiver_HealthBelowThreshold : HediffGiver
    {
        public override void OnIntervalPassed(Pawn pawn, Hediff cause)
        {
            HediffSet hediffSet = pawn.health.hediffSet;
            if (pawn.health.summaryHealth.SummaryHealthPercent <= this.threshold)
            {
                HealthUtility.AdjustSeverity(pawn, this.hediff, hediffSet.BleedRateTotal * 0.001f);
                return;
            }
            HealthUtility.AdjustSeverity(pawn, this.hediff, -0.00033333333f);
        }
        float threshold = 0.85f;

    }
}

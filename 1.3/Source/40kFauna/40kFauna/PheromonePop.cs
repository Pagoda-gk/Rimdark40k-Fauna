using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace AdeptusMechanicus
{
	public class PheromonePop : Pawn
	{
        public override void PostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
        {
            base.PostApplyDamage(dinfo, totalDamageDealt);
            if (this.Dead) return;
            Gas gas = this.Position.GetGas(this.Map);
            if (gas == null || !gas.def.gas.blockTurretTracking)
            {
                GenExplosion.DoExplosion(this.Position, this.Map, radius: 1, DamageDefOf.Smoke, null, -1, -1f, null, null, null, null, ThingDefOf.Gas_Smoke, 1f, 1, false, null, 0f, 1, 0f, false, null, null);
            }
        }
    }	
}
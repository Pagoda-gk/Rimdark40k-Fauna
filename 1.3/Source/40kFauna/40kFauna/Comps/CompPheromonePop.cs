using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using Verse;

namespace AdeptusMechanicus
{
    public class CompProperties_Exploder : CompProperties
    {
        public CompProperties_Exploder()
        {
            this.compClass = typeof(CompExploder);
        }
        public DamageDef damageDef;
    }

    public class CompExploder : ThingComp
    {
        private Pawn Pawn
        {
            get
            {
                Pawn pawn = this.parent as Pawn;
                if (pawn == null)
                {
                    Log.Error("pawn is null");
                }
                return pawn;
            }
        }

        public CompProperties_Exploder Props => this.props as CompProperties_Exploder;

        public override void PostPostApplyDamage(DamageInfo dinfo, float totalDamageDealt)
        {
            base.PostPostApplyDamage(dinfo, totalDamageDealt);
            if (Pawn.Dead) return;
            Gas gas = Pawn.Position.GetGas(Pawn.Map);
            if (gas == null || !gas.def.gas.blockTurretTracking )
            {
                GenExplosion.DoExplosion(Pawn.Position, Pawn.Map, radius: 1, Props.damageDef ?? DamageDefOf.Smoke, null, -1, -1f, null, null, null, null, ThingDefOf.Gas_Smoke, 1f, 1, false, null, 0f, 1, 0f, false, null, null);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using Verse;

namespace PGD_40kFauna
{

    public class CompPropererties_FireStarter : CompProperties
    {
        public CompPropererties_FireStarter()
        {
            this.compClass = typeof(CompFireStarter);
        }
        public float SpreadInterval = 6000f;
        
       

    }

    public class CompFireStarter : ThingComp
    {
            public CompPropererties_FireStarter Props => this.props as CompPropererties_FireStarter;
            public override void CompTick()
            {
                base.CompTick();
                {


                    //this.ticksUntilSmoke--;
                    //if (this.ticksUntilSmoke <= 0)
                    //{
                    //    this.SpawnSmokeParticles();
                    //}

                    if (this.parent is Pawn pawn)
                    {
                        float fireSize = pawn.bodySize;
                        if (/*fireSize > 0.7f && */Rand.Value < fireSize * 0.01f)
                        {
                            MoteMaker.ThrowMicroSparks(this.DrawPos, base.Map);
                            this.ticksSinceSpread++;
                            if ((float)this.ticksSinceSpread >= this.Props.SpreadInterval)
                            {
                                this.TrySpread();
                                this.ticksSinceSpread = 0;
                            }
                        }
                    }
                    if (this.fireSize > 1f)
                    {
                        this.ticksSinceSpread++;
                        if ((float)this.ticksSinceSpread >= this.Props.SpreadInterval)
                        {
                            this.TrySpread();
                            this.ticksSinceSpread = 0;
                        }
                    }
                    if (this.IsHashIntervalTick(150))
                    {
                        this.DoComplexCalcs();
                    }
                    if (this.ticksSinceSpawn >= 7500)
                    {
                        this.TryBurnFloor();
                    }
                }

            }


    }
        protected void TrySpread()
        {
            IntVec3 intVec = base.Position;
            bool flag;
            if (Rand.Chance(0.8f))
            {
                intVec = base.Position + GenRadial.ManualRadialPattern[Rand.RangeInclusive(1, 8)];
                flag = true;
            }
            else
            {
                intVec = base.Position + GenRadial.ManualRadialPattern[Rand.RangeInclusive(10, 20)];
                flag = false;
            }
            if (!intVec.InBounds(base.Map))
            {
                return;
            }
            if (Rand.Chance(FireUtility.ChanceToStartFireIn(intVec, base.Map)))
            {
                if (!flag)
                {
                    CellRect startRect = CellRect.SingleCell(base.Position);
                    CellRect endRect = CellRect.SingleCell(intVec);
                    if (!GenSight.LineOfSight(base.Position, intVec, base.Map, startRect, endRect, null))
                    {
                        return;
                    }
                    ((Spark)GenSpawn.Spawn(ThingDefOf.Spark, base.Position, base.Map, WipeMode.Vanish)).Launch(this, intVec, intVec, ProjectileHitFlags.All, null);
                    return;
                }
                else
                {
                    FireUtility.TryStartFireIn(intVec, base.Map, 0.1f);
                }
            }
        }
}

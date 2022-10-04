using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using Verse;

namespace AdeptusMechanicus
{
    public class CompProperties_Charger : CompProperties
    {
        public float GetChargeChance
        {
            get
            {
                return Mathf.Clamp01(this.Chance);
            }
        }

        public float GetExplodingChargeChance
        {
            get
            {
                return Mathf.Clamp01(this.explodingChance);
            }
        }

        public CompProperties_Charger()
        {
            this.compClass = typeof(CompCharger);
        }
        public float RangeMax = 8f;
        public float RangeMin = 2f;
        public float Chance = 0.5f;
        public float ticksBetweenChargeChance = 100f;
        public bool targetPawns = true;
        public bool targetBuildings = false;
        public bool exploding = false;
        public float explodingChance = 0.2f;
        public float explodingRadius = 2f;
        public bool textMotes = true;
    }

    public class CompCharger : ThingComp
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

        public override void CompTick()
        {
            base.CompTick();
            if (this.Pawn.Spawned)
            {
                /*
                bool flag = Find.TickManager.TicksGame % 10 == 0;
                if (flag)
                {
                    bool flag2 = this.Pawn.Downed && !this.Pawn.Dead;
                    if (flag2)
                    {
                        GenExplosion.DoExplosion(this.Pawn.Position, this.Pawn.Map, Rand.Range(this.explosionRadius * 0.5f, this.explosionRadius * 1.5f), DamageDefOf.Stun, this.Pawn, Rand.Range(6, 10), 0f, null, null, null, null, null, 0f, 1, false, null, 0f, 1, 0f, false);
                    }
                }
                */
                if (Find.TickManager.TicksGame % this.nextLeap == 0 && !this.Pawn.Downed && !this.Pawn.Dead)
                {
                    LocalTargetInfo a = null;
                    if (this.Pawn.CurJob != null && this.Pawn.CurJob.targetA != null)
                    {
                        a = this.Pawn.jobs.curJob.targetA.Thing;
                    }
                    if (a != null && a.Thing != null)
                    {
                        Thing thing = a.Thing;
                        if (thing is Pawn && thing.Spawned && Props.targetPawns)
                        {
                            float lengthHorizontal = (thing.Position - this.Pawn.Position).LengthHorizontal;
                            if (lengthHorizontal <= this.Props.RangeMax && lengthHorizontal > this.Props.RangeMin)
                            {
                                if (Rand.Chance(this.Props.GetChargeChance))
                                {
                                    if (this.CanHitTargetFrom(this.Pawn.Position, thing))
                                    {
                                        this.Attack(thing);
                                    }
                                }
                                else
                                {
                                    if (this.Props.textMotes)
                                    {
                                        if (Rand.Chance(0.5f))
                                        {
                                            MoteMaker.ThrowText(this.Pawn.DrawPos, this.Pawn.Map, "grrr", -1f);
                                        }
                                        else
                                        {
                                            MoteMaker.ThrowText(this.Pawn.DrawPos, this.Pawn.Map, "hsss", -1f);
                                        }
                                    }
                                }
                            }
                        }

                        if (thing is Building && thing.Spawned && Props.targetBuildings)
                        {
                            float lengthHorizontal = (thing.Position - this.Pawn.Position).LengthHorizontal;
                            if (lengthHorizontal <= this.Props.RangeMax && lengthHorizontal > this.Props.RangeMin)
                            {
                                if (Rand.Chance(this.Props.GetChargeChance))
                                {
                                    if (this.CanHitTargetFrom(this.Pawn.Position, thing))
                                    {
                                        this.Attack(thing);
                                    }
                                }
                                else
                                {
                                    if (this.Props.textMotes)
                                    {
                                        if (Rand.Chance(0.5f))
                                        {
                                            MoteMaker.ThrowText(this.Pawn.DrawPos, this.Pawn.Map, "grrr", -1f);
                                        }
                                        else
                                        {
                                            MoteMaker.ThrowText(this.Pawn.DrawPos, this.Pawn.Map, "hsss", -1f);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Attack(LocalTargetInfo target)
        {
            if (target != null && target.Cell != default(IntVec3))
            {
                if (this.Pawn != null && this.Pawn.Position.IsValid && this.Pawn.Spawned && this.Pawn.Map != null && !this.Pawn.Downed && !this.Pawn.Dead && !target.Thing.DestroyedOrNull())
                {
                    this.Pawn.jobs.StopAll(false);
                    ChargingThing chargingThing = (ChargingThing)GenSpawn.Spawn(ThingDef.Named("BETA_DestroyerClassCharge"), this.Pawn.Position, this.Pawn.Map, WipeMode.Vanish);
                    if (target.Thing is Building)
                    {
                        chargingThing.Launch(this.Pawn, target.Cell, this.Pawn, new DamageInfo(DamageDefOf.Blunt, 60f, 0.5f, -1f, this.Pawn));
                    }
                    else
                    {
                        chargingThing.Launch(this.Pawn, target.Cell, this.Pawn);
                    }
                }
            }
        }

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
            this.initialized = true;
            Pawn pawn = this.parent as Pawn;
            this.nextLeap = Mathf.RoundToInt(Rand.Range(this.Props.ticksBetweenChargeChance * 0.75f, 1.25f * this.Props.ticksBetweenChargeChance));
            this.explosionRadius = this.Props.explodingRadius * Rand.Range(0.8f, 1.25f);
        }

        public CompProperties_Charger Props
        {
            get
            {
                return (CompProperties_Charger)this.props;
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<bool>(ref this.initialized, "initialized", true, false);
        }

        private bool CanHitTargetFrom(IntVec3 pawn, LocalTargetInfo target)
        {
            return target.IsValid && target.CenterVector3.InBounds(this.Pawn.Map) && !target.Cell.Fogged(this.Pawn.Map) && target.Cell.Walkable(this.Pawn.Map) && this.TryFindShootLineFromTo(pawn, target, out ShootLine shootLine);
        }

        public bool TryFindShootLineFromTo(IntVec3 root, LocalTargetInfo targ, out ShootLine resultingLine)
        {
            bool result;
            if (targ.HasThing && targ.Thing.Map != this.Pawn.Map)
            {
                resultingLine = default(ShootLine);
                result = false;
            }
            else
            {
                resultingLine = new ShootLine(root, targ.Cell);
                result = GenSight.LineOfSightToEdges(root, targ.Cell, this.Pawn.Map, true, null);
            }
            return result;
        }

        private bool initialized = true;
        public float explosionRadius = 2f;
        private int nextLeap = 0;
    }
}

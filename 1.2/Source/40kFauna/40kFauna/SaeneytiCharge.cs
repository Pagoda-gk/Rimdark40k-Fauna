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
                    Log.Error("pawn is null", false);
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
    [StaticConstructorOnStartup]
    public class ChargingThing : ThingWithComps
    {
        protected int StartingTicksToImpact
        {
            get
            {
                int num = Mathf.RoundToInt((this.origin - this.destination).magnitude / (this.speed / 100f));
                bool flag = num < 1;
                bool flag2 = flag;
                if (flag2)
                {
                    num = 1;
                }
                return num;
            }
        }

        protected IntVec3 DestinationCell
        {
            get
            {
                return new IntVec3(this.destination);
            }
        }

        public virtual Vector3 ExactPosition
        {
            get
            {
                Vector3 b = (this.destination - this.origin) * (1f - (float)this.ticksToImpact / (float)this.StartingTicksToImpact);
                return this.origin + b + Vector3.up * this.def.Altitude;
            }
        }

        public virtual Quaternion ExactRotation
        {
            get
            {
                return Quaternion.LookRotation(this.destination - this.origin);
            }
        }

        public override Vector3 DrawPos
        {
            get
            {
                return this.ExactPosition;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<Vector3>(ref this.origin, "origin", default(Vector3), false);
            Scribe_Values.Look<Vector3>(ref this.destination, "destination", default(Vector3), false);
            Scribe_Values.Look<int>(ref this.ticksToImpact, "ticksToImpact", 0, false);
            Scribe_Values.Look<bool>(ref this.damageLaunched, "damageLaunched", true, false);
            Scribe_Values.Look<bool>(ref this.explosion, "explosion", false, false);
            Scribe_References.Look<Thing>(ref this.assignedTarget, "assignedTarget", false);
            Scribe_References.Look<Pawn>(ref this.pawn, "pawn", false);
            Scribe_Deep.Look<Thing>(ref this.flyingThing, "flyingThing", new object[0]);
        }

        private void Initialize()
        {
            bool flag = this.pawn != null;
            if (flag)
            {
                MoteMaker.ThrowDustPuff(this.pawn.Position, this.pawn.Map, Rand.Range(1.2f, 1.8f));
            }
        }

        public void Launch(Thing launcher, LocalTargetInfo targ, Thing flyingThing, DamageInfo? impactDamage)
        {
            this.Launch(launcher, base.Position.ToVector3Shifted(), targ, flyingThing, impactDamage);
        }

        public void Launch(Thing launcher, LocalTargetInfo targ, Thing flyingThing)
        {
            this.Launch(launcher, base.Position.ToVector3Shifted(), targ, flyingThing, null);
        }

        public void Launch(Thing launcher, Vector3 origin, LocalTargetInfo targ, Thing flyingThing, DamageInfo? newDamageInfo = null)
        {
            bool spawned = flyingThing.Spawned;
            this.pawn = (launcher as Pawn);
            if (spawned)
            {
                flyingThing.DeSpawn(DestroyMode.Vanish);
            }
            this.origin = origin;
            this.impactDamage = newDamageInfo;
            this.flyingThing = flyingThing;
            if (targ.Thing != null)
            {
                this.assignedTarget = targ.Thing;
            }
            this.destination = targ.Cell.ToVector3Shifted();
            this.ticksToImpact = this.StartingTicksToImpact;
            this.Initialize();
        }

        public override void Tick()
        {
            base.Tick();
            Vector3 exactPosition = this.ExactPosition;
            this.ticksToImpact--;
            if (!this.ExactPosition.InBounds(base.Map))
            {
                this.ticksToImpact++;
                base.Position = this.ExactPosition.ToIntVec3();
                this.Destroy(DestroyMode.Vanish);
            }
            else
            {
                base.Position = this.ExactPosition.ToIntVec3();
                if (Find.TickManager.TicksGame % 2 == 0)
                {
                    MoteMaker.ThrowDustPuff(base.Position, base.Map, Rand.Range(0.6f, 0.8f));
                }
                if (this.ticksToImpact <= 0)
                {
                    if (this.DestinationCell.InBounds(base.Map))
                    {
                        base.Position = this.DestinationCell;
                    }
                    this.ImpactSomething();
                }
            }
        }

        public override void Draw()
        {
            if (this.flyingThing != null)
            {
                if (this.flyingThing is Pawn)
                {
                    Vector3 drawPos = this.DrawPos;
                    if (!this.DrawPos.ToIntVec3().IsValid)
                    {
                        return;
                    }
                    Pawn pawn = this.flyingThing as Pawn;
                    pawn.Drawer.DrawAt(this.DrawPos);
                }
                else
                {
                    Graphics.DrawMesh(MeshPool.plane10, this.DrawPos, this.ExactRotation, this.flyingThing.def.DrawMatSingle, 0);
                }
            }
            else
            {
                Graphics.DrawMesh(MeshPool.plane10, this.DrawPos, this.ExactRotation, this.flyingThing.def.DrawMatSingle, 0);
            }
            base.Comps_PostDraw();
        }

        private void DrawEffects(Vector3 pawnVec, Pawn flyingPawn, int magnitude)
        {
            bool flag = !this.pawn.Dead && !this.pawn.Downed;
            bool flag2 = flag;
            if (flag2)
            {
            }
        }

        private void ImpactSomething()
        {
            if (this.assignedTarget != null)
            {
                Pawn pawn = this.assignedTarget as Pawn;
                if (pawn != null && pawn.GetPosture() != PawnPosture.Standing && (this.origin - this.destination).MagnitudeHorizontalSquared() >= 20.25f && Rand.Value > 0.2f)
                {
                    this.Impact(null);
                }
                else
                {
                    this.Impact(this.assignedTarget);
                }
            }
            else
            {
                this.Impact(null);
            }
        }

        protected virtual void Impact(Thing hitThing)
        {
            //        Log.Message(this.Label+" Hit 0");
            if (hitThing == null)
            {
                //        Log.Message(this.Label + " hitThing == null");
                Pawn pawn;
                if ((pawn = (base.Position.GetThingList(base.Map).FirstOrDefault((Thing x) => x == this.assignedTarget) as Pawn)) != null)
                {
                    //        Log.Message(this.Label + " hitThing = pawn");
                    hitThing = pawn;
                }
            }
            if (this.impactDamage != null && hitThing != null)
            {
                //        Log.Message(this.Label + " impactDamage != null");
                hitThing.TakeDamage(this.impactDamage.Value);
            }
            try
            {
                SoundDefOf.Ambient_AltitudeWind.sustainFadeoutTime.Equals(30f);
                GenSpawn.Spawn(this.flyingThing, base.Position, base.Map, WipeMode.Vanish);
                Pawn pawn2 = this.flyingThing as Pawn;
                this.Destroy(DestroyMode.Vanish);
            }
            catch
            {
                GenSpawn.Spawn(this.flyingThing, base.Position, base.Map, WipeMode.Vanish);
                Pawn pawn3 = this.flyingThing as Pawn;
                this.Destroy(DestroyMode.Vanish);
            }
        }

        protected Vector3 origin;
        protected Vector3 destination;
        protected float speed = 28f;
        protected int ticksToImpact;
        protected Thing assignedTarget;
        protected Thing flyingThing;
        public DamageInfo? impactDamage;
        public bool damageLaunched = true;
        public bool explosion = false;
        public int weaponDmg = 0;
        private Pawn pawn;

    }
}

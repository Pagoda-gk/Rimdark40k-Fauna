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

    public class WarpBeing : Pawn ,IThoughtGiver
    {
        public Predicate<Pawn> Predicate => delegate (Pawn t)
        {
            if (t == null)
                Log.Message("t1");
            return false;
            if (t == this)
                Log.Message("t2");
            return false;
            if (!t.Spawned)
                Log.Message("t3");
            return false;
            Pawn pawn1 = t as Pawn;
            if (pawn1 == null)
                Log.Message("t4");
            return false;
            if (pawn1.Dead)
                Log.Message("5");
            return false;
            if (pawn1 is WarpBeing)
                Log.Message("6");
            return false;
            if (this.Faction != null && pawn1.Faction != null)
                Log.Message("7");
            {
                if (this.Faction == pawn1.Faction)
                    Log.Message("8");
                return false;
            }
            if (pawn1.needs == null)
                Log.Message("9");
            return false;
            if (pawn1.needs.mood == null)
                Log.Message("10");
            return false;
            if (pawn1.needs.mood.thoughts == null)
                Log.Message("11");
            return false;
            if (pawn1.needs.mood.thoughts.memories == null)
                Log.Message("12");
            return false;
            Log.Message("target was found");
            return true;
            
        };
        Thought_Memory IThoughtGiver.GiveObservedThought()
        {
            {
            try
            {
                //This finds a suitable target pawn.
                Predicate<Pawn> predicate = this.Predicate;

                Thing thing2 = GenClosest.ClosestThingReachable(this.PositionHeld, this.MapHeld, ThingRequest.ForGroup(ThingRequestGroup.Pawn), PathEndMode.OnCell, TraverseParms.For(this, Danger.Deadly, TraverseMode.PassDoors, false), 15, (Predicate<Thing>)predicate);
                if (thing2 != null && thing2.Position != IntVec3.Invalid)
                {
                    Log.Message("first part of the main method was ok");
                    if (GenSight.LineOfSight(thing2.Position, this.PositionHeld, this.MapHeld))
                    {
                        Log.Message("2nd part of the main method was ok");
                        if (thing2 is Pawn target)
                        {
                            Log.Message("3rd part of the main method was ok");
                            if (target.RaceProps != null)
                            {
                                Log.Message("4th part of the main method was ok");
                                if (!target.RaceProps.IsMechanoid)
                                {
                                    Log.Message("5th part of the main method was ok");
                                    ThoughtDef defToImplement = PGD_40kFaunaDefOf.PGD_ObservedWarpBeing;
                                    if (defToImplement == null) return null;
                                    Thought_MemoryObservation thought_MemoryObservation;
                                    thought_MemoryObservation = (Thought_MemoryObservation)ThoughtMaker.MakeThought(defToImplement);
                                    thought_MemoryObservation.Target = this;
                                    target.needs.mood.thoughts.memories.TryGainMemory(thought_MemoryObservation);

                                }
                            }
                        }
                    }

                }
            }
                catch (NullReferenceException)
                { }
                throw new NotImplementedException();

            }
        }




    }
}
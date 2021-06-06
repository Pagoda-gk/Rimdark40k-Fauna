using RimWorld;
using Verse;
using HarmonyLib;
using System.Reflection;
using System.Collections.Generic;
using System;
using Verse.AI;
using System.Text;
using System.Linq;
using Verse.AI.Group;
using RimWorld.Planet;
using UnityEngine;


namespace PGD_40kFauna
{

    //// ObserveSurroundingThings
    //[HarmonyPatch(typeof(PawnObserver), "ObserveSurroundingThings")]
    //public static class Khymera_PawnObserver_ObserveSurroundingThings_Patch
    //{
    //    [HarmonyPostfix]
    //    public static void ObserveSurroundingThingsPostfix(PawnObserver __instance, Pawn ___pawn)
    //    {
    //        //    Log.Message(string.Format("ObserveSurroundingThingsPostfix {0}", pawn.LabelShortCap));
    //        if (!___pawn.health.capacities.CapableOf(PawnCapacityDefOf.Sight))
    //        {
    //            //    Log.Message("Blind");
    //            return;
    //        }
    //        float num = ___pawn.health.capacities.GetLevel(PawnCapacityDefOf.Sight);
    //        //    Log.Message(string.Format("Sight: {0}", num));
    //        Map map = ___pawn.Map;
    //        //    List<Thing> thingList = intVec.GetThingList(map).FindAll(x => x.def.thingClass == typeof(Pawn) && (Pawn)x != pawn);
    //        List<Pawn> thingList = map.mapPawns.AllPawns.Where(x => x != ___pawn && x.isKhymera() && GenSight.LineOfSight(x.Position, ___pawn.Position, map, true, null, 0, 0) && ___pawn.Position.DistanceTo(x.Position) <= (20 * num)).ToList();

    //        if (!thingList.NullOrEmpty())
    //        {
    //            for (int i = 0; i < thingList.Count; i++)
    //            {
    //                Pawn observed = (Pawn)thingList[i];
    //                if (observed == null)
    //                {
    //                    return;
    //                }
    //                if (!observed.isKhymera(out Comp_Xenomorph thoughtGiver))
    //                {
    //                    return;
    //                }
    //                //    Log.Message(string.Format("{0} observed {1}", pawn.LabelShortCap, observed.LabelShortCap));
    //                if (thoughtGiver != null)
    //                {
    //                    Thought_Memory thought_Memory = thoughtGiver.GiveObservedThought();
    //                    if (thought_Memory != null)
    //                    {
    //                        //Log.Message(string.Format("{0} TryGainMemory {1}", pawn.LabelShortCap, thought_Memory.LabelCap));
    //                        ___pawn.needs.mood.thoughts.memories.TryGainMemory(thought_Memory, observed);
    //                    }
    //                    //    else Log.Message("thought_Memory == null");
    //                }
    //                //    else Log.Message(string.Format("thoughtGiver {0} == null", thingList[i].LabelShortCap));
    //            }
    //        }
    //        //    else Log.Message("thingList.NullOrEmpty");
    //    }
    //}

}
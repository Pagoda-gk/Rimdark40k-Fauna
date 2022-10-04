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

namespace AdeptusMechanicus
{
    public class WarpBeing : Pawn, IObservedThoughtGiver
    {
        public HistoryEventDef GiveObservedHistoryEvent(Pawn observer)
        {
            return null;
        }

        public Thought_Memory GiveObservedThought(Pawn observer)
        {
            Thought_MemoryObservation thought_MemoryObservation;
            ThoughtDef defToImplement = FaunaThoughtDefOf.OG_ObservedWarpBeing;
            if (defToImplement == null) return null;
            thought_MemoryObservation = (Thought_MemoryObservation)ThoughtMaker.MakeThought(defToImplement);
            thought_MemoryObservation.Target = this;
            return thought_MemoryObservation;
        }


    }
}
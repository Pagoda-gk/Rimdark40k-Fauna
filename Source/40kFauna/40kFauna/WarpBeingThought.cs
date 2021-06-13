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

    public class WarpBeing : Pawn, IThoughtGiver
    {
        Thought_Memory IThoughtGiver.GiveObservedThought()
        {
            Thought_MemoryObservation thought_MemoryObservation;
            ThoughtDef defToImplement = PGD_40kFaunaDefOf.PGD_ObservedWarpBeing;
            if (defToImplement == null) return null;
            thought_MemoryObservation = (Thought_MemoryObservation)ThoughtMaker.MakeThought(defToImplement);
            thought_MemoryObservation.Target = this;
            return thought_MemoryObservation;
        }


    }
}
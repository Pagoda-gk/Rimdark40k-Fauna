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
    public class DeathActionWorker_WarpBeing : DeathActionWorker
    {
        public override void PawnDied(Corpse corpse)
        {
            if (corpse != null)
            {
                MoteMaker.MakeAttachedOverlay(corpse, ThingDefOf.Mote_PsycastSkipFlashEntry, Vector3.zero, 1f, -1f).detachAfterTicks = 5;
                corpse.Destroy();
                
            }
        }
    }
}
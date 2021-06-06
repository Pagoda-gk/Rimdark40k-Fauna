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
    public class DeathActionWorker_WarpSpawn : DeathActionWorker
    {
        public override void PawnDied(Corpse corpse)
        {
            if (corpse != null)
            {
                corpse.Destroy();
            }
        }
    }
}
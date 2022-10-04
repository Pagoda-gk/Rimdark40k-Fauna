using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace AdeptusMechanicus
{

    [DefOf]
    public static class FaunaPawnKindDefOf
	{
		static FaunaPawnKindDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(FaunaPawnKindDefOf));
		}
		public static PawnKindDef OG_Xenos_XothicBloodLocustS; 
		public static PawnKindDef OG_Xenos_Khymera;

	}
}

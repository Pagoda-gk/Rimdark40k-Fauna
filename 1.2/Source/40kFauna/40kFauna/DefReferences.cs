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

namespace PGD_40kFauna
{
	[DefOf]
	public static class PGD_40kFaunaDefOf
	{
		static PGD_40kFaunaDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(PGD_40kFaunaDefOf));
		}
		public static ThingDef PGD_Grox;
		public static ThingDef PGD_Khymera;
		public static HediffDef PGD_DjemjaFalak;
		public static ThoughtDef PGD_ObservedWarpBeing;

	}
}

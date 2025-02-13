﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Utils;
using DragonChanges.Content;
using DragonChanges.New_Archetypes;
using DragonChanges.New_Backgrounds;
using DragonChanges.New_Classes;
using DragonChanges.New_Classes.Redditor;
using DragonChanges.New_Classes.Swordmaster;
using DragonChanges.NewStuff;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using UnityModManagerNet;

namespace DragonChanges
{
#if DEBUG
    [EnableReloading]
#endif
    public static class Main
    {
        internal static Harmony HarmonyInstance;
        internal static UnityModManager.ModEntry.ModLogger log;
        internal static UnityModManager.ModEntry entry;

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            log = modEntry.Logger;
            entry = modEntry;
            modEntry.OnGUI = OnGUI;
            HarmonyInstance = new Harmony(modEntry.Info.Id);
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            return true;
        }

        public static void OnGUI(UnityModManager.ModEntry modEntry)
        {

        }

        [HarmonyPatch(typeof(BlueprintsCache))]
        public static class BlueprintsCaches_Patch
        {
            private static bool Initialized = false;

            [HarmonyPriority(Priority.Last)]
            [HarmonyAfter()]
            [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
            public static void Init_Postfix()
            {
                try
                {
                    if (Initialized)
                    {
                        log.Log("Already initialized blueprints cache.");
                        return;
                    }
                    Initialized = true;

                    log.Log("Checking for mods for compatibility patches");
                    Utils.ModCompat.CheckForMods();
                    Utils.Settings.InitializeSettings();
                    log.Log("Patching blueprints.");
                    AllBackgrounds.Configure();
                    Feature.Configure();
                    AllClasses.Configure();
                    AllArchetypes.Configure();
                    // no group configures for these yet
                    AlterMod.PatchHorse();
                    Drakes.PatchDrakes();
                    Various.PatchHippogriff();
                }
                catch (Exception e)
                {
                    log.Log(string.Concat("Failed to initialize.", e));
                }
            }
        }
    }
}

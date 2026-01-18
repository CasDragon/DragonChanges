using System;
using System.IO;
using System.Reflection;
using BlueprintCore.Utils;
using DragonChanges.NewStuff;
using DragonLibrary.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using UnityModManagerNet;

namespace DragonChanges
{
    public static class Main
    {
        internal static Harmony HarmonyInstance;
        internal static UnityModManager.ModEntry.ModLogger log;
        internal static UnityModManager.ModEntry entry;

        [DragonSetting(SettingCategories.Various, "darthicons", "Enable replacing my perfect icons with icons that DarthParametric has provided me.", false)]
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
            [HarmonyAfter("DragonLibrary")]
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

                    log.Log("Getting mod folder path for localization ");
                    string modfolder = new FileInfo(entry.Assembly.Location).Directory.FullName;
                    log.Log("Creating localization file");
                    LocalizedStringHelper.CreateLocalizationFile(modfolder, entry);
                    LocalizationTool.LoadLocalizationPacks(
                        Path.Combine(modfolder, "NewLocalizedStrings.json"),
                        Path.Combine(modfolder, "LocalizedStrings.json"));

                    SettingsAction.InitializeSettings("dragonchanges", "DragonChanges", entry);
                    log.Log("Patching blueprints.");
                    AneviaVendor.ConfigureStart();
                    DragonConfigureAction.DoPatches(entry);
                    AneviaVendor.ConfigureEnd();
                }
                catch (Exception e)
                {
                    log.Log(string.Concat("Failed to initialize.", e));
                }
            }
        }
    }
}

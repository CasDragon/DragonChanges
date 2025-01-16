using Kingmaker.Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityModManagerNet;

namespace DragonChanges.Utils
{
    internal class ModCompat
    {
        public static bool microscopic = false;
        public static bool expandedcontent = false;
        public static bool homebrewarchetypes = false;
        public static bool tttbase = false;
        public static bool cop = false;

        public static void CheckForMods()
        {
            microscopic = IsMicroscopicEnabled();
            LogModState(microscopic, "Microscopic Content Expansion");
            expandedcontent = IsECEnabled();
            LogModState(expandedcontent, "Expanded Content");
            homebrewarchetypes = IsHAEnabled();
            LogModState(homebrewarchetypes, "Homebrew Archetypes");
            tttbase = isTTTBaseEnabled();
            LogModState(tttbase, "TabletopTweaks-Base");
            cop = isCOPEnabled();
            LogModState(cop, "CharacterOptionsPlus");
        }
        public static bool isCOPEnabled()
        {
            Main.log.Log("Checking for CO+");
            return IsModEnabled("CharacterOptionsPlus");
        }
        public static bool isTTTBaseEnabled()
        {
            Main.log.Log("Checking for TTT-Base");
            return IsModEnabled("TabletopTweaks-Base");
        }
        public static bool IsHAEnabled()
        {
            Main.log.Log("Checking for HomebrewArchetypes");
            return IsModEnabled("HomebrewArchetypes", "owlcat");
        }
        public static bool IsMicroscopicEnabled()
        {
            Main.log.Log("Checking for Microscopic Content Expansion");
            return IsModEnabled("MicroscopicContentExpansion");
        }
        public static bool IsECEnabled()
        {
            Main.log.Log("Checking for Expanded Content");
            return IsModEnabled("ExpandedContent");
        }
        public static bool IsModEnabled(string modName, string modtype="umm")
        {
            if (modtype=="umm")
                return UnityModManager.modEntries.Where(mod => mod.Info.Id.Equals(modName) && mod.Enabled && !mod.ErrorOnLoading).Any();
            if (modtype=="owlcat")
                return OwlcatModificationsManager.Instance.AppliedModifications.Any(x => x.Manifest.UniqueName == modName);
            return false;
        }
        public static void LogModState(bool mod, string modname)
        {
            if (mod)
            {
                Main.log.Log($"{modname} is found and enabled");
            }
            else
            {
                Main.log.Log($"{modname} wasn't found, disabling compatiblity patches");
            }
        }
    }
}

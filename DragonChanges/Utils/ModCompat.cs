using Kingmaker.Modding;
using System.Linq;
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
        public static bool pp = false;
        public static bool randomequipment = false;
        public static bool scalingequip = false;

        public static void CheckForMods()
        {
            microscopic = IsModEnabled("MicroscopicContentExpansion");
            expandedcontent = IsModEnabled("ExpandedContent");
            homebrewarchetypes = IsModEnabled("HomebrewArchetypes", "owlcat");
            tttbase = IsModEnabled("TabletopTweaks-Base");
            cop = IsModEnabled("CharacterOptionsPlus");
            pp = IsModEnabled("PrestigePlugs");
            randomequipment = IsModEnabled("RandomEquipment");
            scalingequip = IsModEnabled("WrathScalingItemDCs");
        }
        public static bool IsModEnabled(string modName, string modtype = "umm")
        {
            Main.log.Log($"Checking for {modName}");
            bool found = false;
            if (modtype == "umm")
                found = UnityModManager.modEntries.Where(mod => mod.Info.Id.Equals(modName) && mod.Enabled && !mod.ErrorOnLoading).Any();
            if (modtype == "owlcat")
                found = OwlcatModificationsManager.Instance.AppliedModifications.Any(x => x.Manifest.UniqueName == modName);
            LogModState(found, modName);
            return found;
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

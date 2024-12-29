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

        public static void CheckForMods()
        {
            microscopic = IsMicroscopicEnabled();
            expandedcontent = IsECEnabled();
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
        public static bool IsModEnabled(string modName)
        {
            return UnityModManager.modEntries.Where(mod => mod.Info.Id.Equals(modName) && mod.Enabled && !mod.ErrorOnLoading).Any();
        }
        public static void LogModState(bool mod, string modname)
        {
            if (mod)
            {
                Main.log.Log("{modname} is found and enabled");
            }
            else
            {
                Main.log.Log("{modname} wasn't found, disabling compatiblity patches");
            }
        }
    }
}

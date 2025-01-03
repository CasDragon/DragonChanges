﻿using DragonChanges;
using Kingmaker.Localization;
using ModMenu.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityModManagerNet;

namespace DragonChanges.Utils
{
    internal class Settings
    {
        private static readonly string RootKey = "dragonchanges";

        public static void InitializeSettings()
        {
            ModMenu.ModMenu.AddSettings(
                SettingsBuilder.New(RootKey, CreateString(GetKey("dc-title"), "DragonChanges"))
                    .SetMod(Main.entry)
                    .AddAnotherSettingsGroup(GetKey("modcompat"), CreateString(GetKey("modcompat"), "Compatibility"))
                    .AddToggle(
                        Toggle.New(GetKey("mc-microscopic-horse"), defaultValue: true, CreateString("mc-microscopic-horse-toggle", "Adds the Nightmare animal companion (MicroscopicContent) to other pet lists")))
                    .AddToggle(
                        Toggle.New(GetKey("ec-drakes"), defaultValue: false, CreateString("ec-drakes-toggle", "Super secret option to buff drakes from Expanded Content")))
                    .AddAnotherSettingsGroup(GetKey("various"), CreateString(GetKey("various-group"), "Various"))
                    .AddToggle(
                        Toggle.New(GetKey("hippogriff"), defaultValue: true, CreateString("hippogriff-toggle", "Adds the Hippogriff  to other pet lists")))
                    .AddAnotherSettingsGroup(GetKey("newcontent"), CreateString(GetKey("newcontent-group"), "New Content"))
                    .AddToggle(
                        Toggle.New(GetKey("powerfulthrow"), defaultValue: true, CreateString("powerfulthrow-toggle", "Adds the feat Powerful Throw"))));
        }
        public static T GetSetting<T>(string key)
        {
            try
            {
                return ModMenu.ModMenu.GetSettingValue<T>(GetKey(key));
            }
            catch (Exception ex)
            {
                Main.log.Error(ex.ToString());
                return default(T);
            }
        }
        private static LocalizedString CreateString(string partialkey, string text)
        {
            return Helpers.CreateString(GetKey(partialkey), text);
        }
        private static string GetKey(string partialKey)
        {
            return $"{RootKey}.{partialKey}";
        }

    }
    public static class Helpers
    {
        private static Dictionary<string, LocalizedString> textToLocalizedString = new Dictionary<string, LocalizedString>();
        public static LocalizedString CreateString(string key, string value)
        {
            // See if we used the text previously.
            // (It's common for many features to use the same localized text.
            // In that case, we reuse the old entry instead of making a new one.)
            if (textToLocalizedString.TryGetValue(value, out LocalizedString localized))
            {
                return localized;
            }
            var strings = LocalizationManager.CurrentPack?.m_Strings;
            if (strings!.TryGetValue(key, out var oldValue) && value != oldValue.Text)
            {
                Main.log.Log($"Info: duplicate localized string `{key}`, different text.");
            }
            var sE = new Kingmaker.Localization.LocalizationPack.StringEntry();
            sE.Text = value;
            strings[key] = sE;
            localized = new LocalizedString
            {
                m_Key = key
            };
            textToLocalizedString[value] = localized;
            return localized;
        }
    }
}


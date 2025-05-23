﻿using Kingmaker.Localization;
using ModMenu.Settings;
using System;
using System.Collections.Generic;

namespace DragonChanges.Utils
{
    /*internal class Settings
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
                    .AddToggle(
                        Toggle.New(GetKey("co-pairedopp"), defaultValue: false, CreateString("co-pairedopp-toggle", "Adds Paired Opportunist teamwork feat to more selections")))
                    .AddAnotherSettingsGroup(GetKey("various"), CreateString(GetKey("various-group"), "Various"))
                    .AddToggle(
                        Toggle.New(GetKey("hippogriff"), defaultValue: true, CreateString("hippogriff-toggle", "Adds the Hippogriff  to other pet lists")))
                    .AddToggle(
                        Toggle.New(GetKey("autometamagics"), defaultValue: true, CreateString("autometamagics-toggle", "Adds feats that grant autometamagic to Mythic Abilities")))
                    .AddAnotherSettingsGroup(GetKey("newcontent"), CreateString(GetKey("newcontent-group"), "New Content"))
                    .AddToggle(
                        Toggle.New(GetKey("newdeities"), defaultValue: true, CreateString("newdeities-toggle", "Adds various new deities.")))
                    .AddToggle(
                        Toggle.New(GetKey("powerfulthrow"), defaultValue: true, CreateString("powerfulthrow-toggle", "Adds the feat Powerful Throw")))
                    .AddToggle(
                        Toggle.New(GetKey("powerfulwings"), defaultValue: true, CreateString("PowerfulWings-toggle", "Adds the feat Powerful Wings")))
                    .AddToggle(
                        Toggle.New(GetKey("gravesingerplus"), defaultValue: true, CreateString("gravesingerplus-toggle", "Adds the feat GraveSingerPlus")))
                    .AddToggle(
                        Toggle.New(GetKey("undeadmount"), defaultValue: true, CreateString("undeadmount-toggle", "Adds a new undead mount, and then adds it to mount selections")))
                    .AddToggle(
                        Toggle.New(GetKey("griffonmount"), defaultValue: true, CreateString("griffonmount-toggle", "Adds a new griffon mount, and then adds it to mount selections")))
                    .AddToggle(
                        Toggle.New(GetKey("unicornmount"), defaultValue: true, CreateString("unicornmount-toggle", "Adds a new unicorn mount, and then adds it to mount selections")))
                    .AddAnotherSettingsGroup(GetKey("newspells"), CreateString(GetKey("newspells-group"), "New Spells"))
                    .AddToggle(
                        Toggle.New(GetKey("heavenray"), defaultValue: true, CreateString(GetKey("heavenfire-toggle"), "Enable the Heavenfire Ray spell")))
                    .AddToggle(
                        Toggle.New(GetKey("hellball"), defaultValue: true, CreateString(GetKey("hellball-toggle"), "Enable the HellBall spell, added from a Mythic Ability")))
                    .AddToggle(
                        Toggle.New(GetKey("hellballicon"), defaultValue: false, CreateString(GetKey("hellhellballiconball-toggle"), "Change the HellBall spell icon to the WRONG icon, which is worse, for Tomek (requires game restart)")))
                    .AddAnotherSettingsGroup(GetKey("newitems"), CreateString(GetKey("newitems-group"), "New Items/Weapons/Armor"))
                    .AddToggle(
                        Toggle.New(GetKey("storvalsthunder"), defaultValue: true, CreateString(GetKey("storvalsthunder-toggle"), "Enable the Storval's Thunder weapon and the Storval's Fang shield.")))
                    .AddToggle(
                        Toggle.New(GetKey("thwarter"), defaultValue: true, CreateString("thwarter-toggle", "Adds the Thwarter tongi item")))
                    .AddToggle(
                        Toggle.New(GetKey("memerings"), defaultValue: true, CreateString("memerings-toggle", "Adds adds a bunch of meme rings")))
                    .AddAnotherSettingsGroup(GetKey("newarchetypes"), CreateString(GetKey("newarchetypes-group"), "New Archetypes"))
                    .AddToggle(
                        Toggle.New(GetKey("swordmastertengu"), defaultValue: true, CreateString(GetKey("swordmastertengu-toggle"), "Enable the Swordmaster (Tengu) archetype")))
                    .AddAnotherSettingsGroup(GetKey("newbackgrounds"), CreateString(GetKey("newbackgrounds-group"), "New Backgrounds"))
                    .AddToggle(
                        Toggle.New(GetKey("wanderer"), defaultValue: true, CreateString(GetKey("wanderer-toggle"), "Enable the Wanderer (Lycanothrpy) background")))
                    .AddAnotherSettingsGroup(GetKey("newclasses"), CreateString(GetKey("newclasses-group"), "New Classes"))
                    .AddToggle(
                        Toggle.New(GetKey("redditor"), defaultValue: false, CreateString(GetKey("redditor-toggle"), "Enable the Redditor class  (which is a meme class I made, don't take too seriously)")))
                    .AddToggle(
                        Toggle.New(GetKey("swordmaster"), defaultValue: false, CreateString(GetKey("swordmaster-toggle"), "Enable the Swordmaster class (WIP)"))));
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
        private static Dictionary<string, LocalizedString> textToLocalizedString = [];
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
            var sE = new LocalizationPack.StringEntry
            {
                Text = value
            };
            strings[key] = sE;
            localized = new LocalizedString
            {
                m_Key = key
            };
            textToLocalizedString[value] = localized;
            return localized;
        }
    }*/
}


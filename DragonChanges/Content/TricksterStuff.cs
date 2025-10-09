using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonChanges.Content
{
    internal class TricksterStuff
    {
        const string settingName = "statfocusspeedbuff";
        const string settingDescription = "Buffs Stat Focus - Speed (Trickster feat) from a +1 bonus to speed to a +10.";
        [DragonConfigure]
        [DragonSetting(SettingCategories.Various, settingName, settingDescription)]
        public static void PatchTricksterStatFocusSpeed()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log("Patching TricksterStatFocusSpeed to actually be useful.");
                FeatureConfigurator.For(FeatureRefs.TricksterStatFocusSpeed)
                    .EditComponent<AddStatBonus>(c => c.Value = 10)
                    .Configure();
            }
        }
    }
}

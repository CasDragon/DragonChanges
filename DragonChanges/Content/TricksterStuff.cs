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
        const string settingName = "statfocusbuff";
        const string settingDescription = "Buffs Stat Focus (Trickster feat) useless variants, such as Speed and Hit Points (Seriously, just a +1 to speed?) .";
        const string updatedDescription = "You gain a +15 mythic {g|Encyclopedia:Bonus}bonus{/g} to your speed.";
        [DragonConfigure]
        [DragonSetting(SettingCategories.Various, settingName, settingDescription)]
        public static void PatchTricksterStatFocusSpeed()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log("Patching TricksterStatFocusSpeed to actually be useful.");
                FeatureConfigurator.For(FeatureRefs.TricksterStatFocusSpeed)
                    .SetDescription(updatedDescription)
                    .EditComponent<AddStatBonus>(c => c.Value = 15)
                    .Configure();
            }
        }
        const string hpupdatedDescription = "You gain a +10 mythic {g|Encyclopedia:Bonus}bonus{/g} to your hit points.";
        [DragonConfigure]
        public static void PatchTricksterStatFocusHP()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log("Patching TricksterStatFocusSpeed to actually be useful.");
                FeatureConfigurator.For(FeatureRefs.TricksterStatFocusHP)
                    .SetDescription(hpupdatedDescription)
                    .EditComponent<AddStatBonus>(c => c.Value = 10)
                    .Configure();
            }
        }
    }
}

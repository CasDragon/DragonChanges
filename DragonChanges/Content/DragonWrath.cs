using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UI.AbilityTarget;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using static TabletopTweaks.Core.MechanicsChanges.MetamagicExtention;

namespace DragonChanges.Content
{
    internal class DragonWrath
    {
        // edit
        internal const string settingName = "dragonwrathbuff";
        internal const string settingDescription = "Buffs the Golden Dragon spell 'Dragon Wrath' to actually have metamagics be able to be applied to it, via wands or Completely Normal Spell.";
        // don't edit
        [DragonConfigure]
        [DragonSetting(SettingCategories.NewAbilities, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{settingName} feature enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{settingName} disabled");
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
        }
        public static void ConfigureEnabled()
        {
            Main.log.Log("Buffing DragonWrath spell to have available metamagics.");
            Blueprint<BlueprintReference<BlueprintAbility>>[] spells = [AbilityRefs.DragonWrath, AbilityRefs.DragonWrathGold, AbilityRefs.DragonWrathGoldCorrupted];
            Metamagic metas = Metamagic.CompletelyNormal | Metamagic.Reach | Metamagic.Empower | Metamagic.Bolstered | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Intensified;
            if (ModCompat.tttbase)
            {
                metas = metas | (Metamagic)(CustomMetamagic.Burning | CustomMetamagic.ElementalAcid |
                    CustomMetamagic.ElementalCold | CustomMetamagic.ElementalElectricity |
                    CustomMetamagic.ElementalFire | CustomMetamagic.Flaring);
            }
            foreach (var spell in spells)
            {
                AbilityConfigurator.For(spell)
                    .SetAvailableMetamagic(metas)
                    .Configure();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using DragonChanges.BPCoreExtensions;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.EntitySystem.Stats;

namespace DragonChanges.NewStuff
{
    internal class BowDexDamage
    {
        // edit
        internal const string feature = "SuperDexShot";
        internal const string featureguid = Guids.BowDexDamage;
        internal const string settingName = "bowdexdamage";
        internal const string settingDescription = "A new mythic ability that grants DEX damage to bows instead of STR.";
        // don't edit
        internal const string featurename = $"{feature}.name";
        internal const string featuredescription = $"{feature}.description";
        [DragonConfigure]
        [DragonSetting(settingCategories.NewFeatures, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{feature} feature enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{feature} disabled, configuring dummy");
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .SetIsClassFeature(true)
                .AddToGroups(Kingmaker.Blueprints.Classes.FeatureGroup.MythicAbility)
                .AddWorkingAttackStatReplacementForWeaponGroup(StatType.Dexterity, WeaponFighterGroupFlags.Bows)
                .Configure();
        }
    }
}

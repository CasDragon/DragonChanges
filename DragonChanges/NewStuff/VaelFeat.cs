using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewStuff
{
    internal class VaelFeat
    {
        // edit
        internal const string feature = "VaelsSuperSpecialFeat";
        internal const string featureguid = Guids.VaelFeat1;
        internal const string settingName = "vaelmythicscalingfeat";
        internal const string settingDescription = "Adds "Mythic Body and Mind," a meme feat for Vael in Discord, which gives +1 per MR as an untyped bonus to all ability scores.";
        internal const string featurename = "Mythic Body and Mind";
        internal const string featuredescription = "Your baseline attributes grow alongside your mythic powers.\nYou gain a bonus to all ability scores equal to your mythic rank.";
        // don't edit
        [DragonLocalizedString(featurenamekey, featurename)]
        internal const string featurenamekey = $"{feature}.name";
        [DragonLocalizedString(featuredescriptionkey, featuredescription)]
        internal const string featuredescriptionkey = $"{feature}.description";
        [DragonConfigure]
        [DragonSetting(SettingCategories.NewFeatures, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
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
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurenamekey)
                .SetDescription(LocalizedStringHelper.disabledcontentstring)
                .Configure();
        }
        public static void ConfigureEnabled()
        {
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurenamekey)
                .SetDescription(featuredescriptionkey)
                .AddContextRankConfig(ContextRankConfigs.MythicLevel())
                .AddContextStatBonus(descriptor: ModifierDescriptor.UntypedStackable,
                    stat: StatType.Strength,
                    value: ContextValues.Rank())
                .AddContextStatBonus(descriptor: ModifierDescriptor.UntypedStackable,
                    stat: StatType.Dexterity,
                    value: ContextValues.Rank())
                .AddContextStatBonus(descriptor: ModifierDescriptor.UntypedStackable,
                    stat: StatType.Constitution,
                    value: ContextValues.Rank())
                .AddContextStatBonus(descriptor: ModifierDescriptor.UntypedStackable,
                    stat: StatType.Intelligence,
                    value: ContextValues.Rank())
                .AddContextStatBonus(descriptor: ModifierDescriptor.UntypedStackable,
                    stat: StatType.Wisdom,
                    value: ContextValues.Rank())
                .AddContextStatBonus(descriptor: ModifierDescriptor.UntypedStackable,
                    stat: StatType.Charisma,
                    value: ContextValues.Rank())
                .AddRecalculateOnStatChange(stat: StatType.HitPoints)
                .AddToGroups(FeatureGroup.MythicAbility)
                .Configure();
        }
    }
}

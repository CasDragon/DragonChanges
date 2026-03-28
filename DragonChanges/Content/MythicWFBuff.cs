using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonLibrary.BPCoreExtensions;
using DragonLibrary.NewComponents;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
namespace DragonChanges.Content
{
    internal class MythicWFBuff
    {

        internal const string settingName = "buffmythicweaponfocus";
        internal const string settingDescription = "Buffs Mythic Weapon Focus/Weapon Specialization to add a scaling bonus equal to half your Mythic Rank.";
        [DragonConfigure]
        [DragonSetting(SettingCategories.Various, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log("Buffing Mythic WF/WS");
                ParametrizedFeatureConfigurator.For(ParametrizedFeatureRefs.WeaponFocusMythicFeat)
                    .AddComponent( new FeatMythicScaling()
                        {
                        Value = 1,
                        Stat = StatType.AdditionalAttackBonus,
                        ParametrizedFeature = ParametrizedFeatureRefs.WeaponFocus.Reference.Get().ToReference<BlueprintParametrizedFeatureReference>(),
                        MythicParametrizedFeature = ParametrizedFeatureRefs.WeaponFocusGreater.Reference.Get().ToReference<BlueprintParametrizedFeatureReference>(),
                        ScalingType = "Half",
                        Descriptor = ModifierDescriptor.UntypedStackable
                        })
                    .Configure();
                ParametrizedFeatureConfigurator.For(ParametrizedFeatureRefs.WeaponSpecializationMythicFeat)
                    .AddComponent(new FeatMythicScaling()
                    {
                        Value = 1,
                        Stat = StatType.AdditionalDamage,
                        ParametrizedFeature = ParametrizedFeatureRefs.WeaponSpecialization.Reference.Get().ToReference<BlueprintParametrizedFeatureReference>(),
                        MythicParametrizedFeature = ParametrizedFeatureRefs.WeaponSpecializationGreater.Reference.Get().ToReference<BlueprintParametrizedFeatureReference>(),
                        ScalingType = "Half",
                        Descriptor = ModifierDescriptor.UntypedStackable
                    })
                    .Configure();
            }
        }
    }
}

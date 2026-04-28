using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.BPCoreExtensions;
using DragonLibrary.NewComponents;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
namespace DragonChanges.Content
{
    internal class MythicWFBuff
    {

        internal const string settingName = "buffmythicweaponfocus";
        internal const string settingDescription = "Buffs Mythic Weapon Focus/Weapon Specialization to add a scaling bonus equal to half your Mythic Rank.";
        internal const string wfkey = "dragonweaponfocus";
        internal const string wskey = "dragonweaponspecial";
        [DragonLocalizedString(wfkey, weaponfocusdescription)]
        internal const string weaponfocusdescription = "When using your chosen weapon, you gain a bonus equal to one plus half your mythic rank on attack rolls. If you have Greater Weapon Focus, the bonus is equal to one plus your full mythic rank instead. This stacks with the bonus from Weapon Focus and Greater Weapon Focus.";
        [DragonLocalizedString(wskey, weaponspecialdescription)]
        internal const string weaponspecialdescription = "When using your chosen weapon, you gain a bonus equal to one plus half your mythic rank on damage rolls. If you have Greater Weapon Specialization, the bonus is equal to one plus your full mythic rank instead. This stacks with the bonus from Weapon Specialization and Greater Weapon Specialization.";

        [DragonConfigure]
        [DragonSetting(SettingCategories.Various, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log("Buffing Mythic WF/WS");
                var x = ParametrizedFeatureRefs.WeaponFocus.Reference.Get();
                x.GetComponent<WeaponFocusParametrized>().m_MythicFocus = null;
                var z = ParametrizedFeatureRefs.WeaponFocusGreater.Reference.Get();
                x.GetComponent<WeaponFocusParametrized>().m_MythicFocus = null;
                ParametrizedFeatureConfigurator.For(ParametrizedFeatureRefs.WeaponFocusMythicFeat)
                    .AddComponent( new FeatMythicScaling()
                        {
                        Value = 1,
                        Stat = StatType.AdditionalAttackBonus,
                        ParametrizedFeature = x.ToReference<BlueprintParametrizedFeatureReference>(),
                        MythicParametrizedFeature = z.ToReference<BlueprintParametrizedFeatureReference>(),
                        ScalingType = "Half",
                        Descriptor = ModifierDescriptor.UntypedStackable
                        })
                    .SetDescription(wfkey)
                    .Configure();
                var y = ParametrizedFeatureConfigurator.For(ParametrizedFeatureRefs.WeaponSpecializationMythicFeat)
                    .AddComponent(new FeatMythicScaling()
                    {
                        Value = 1,
                        Stat = StatType.AdditionalDamage,
                        ParametrizedFeature = ParametrizedFeatureRefs.WeaponSpecialization.Reference.Get().ToReference<BlueprintParametrizedFeatureReference>(),
                        MythicParametrizedFeature = ParametrizedFeatureRefs.WeaponSpecializationGreater.Reference.Get().ToReference<BlueprintParametrizedFeatureReference>(),
                        ScalingType = "Half",
                        Descriptor = ModifierDescriptor.UntypedStackable
                    })
                    .SetDescription(wskey)
                    .Configure();
                DragonHelpers.RemoveComponent<WeaponSpecializationParametrized>(y);
            }
        }

        internal const string settingNameViggy = "buffmythiclightarmorviggy";
        internal const string settingDescriptionViggy = "For Viggy, buffs Mythic Light Armor Assualt to work with Fighter's Finesse. Basically cheating.";
        [DragonConfigure]
        [DragonSetting(SettingCategories.Various, settingNameViggy, settingDescriptionViggy, false)]
        public static void MythicLightArmorBuff()
        {
            if (SettingsAction.GetSetting<bool>(settingNameViggy))
            {
                Main.log.Log("Buffing Mythic Assault for Light Armor.");
                BlueprintFeatureReference finesseref = FeatureRefs.FightersFinesse.Reference.Get().ToReference<BlueprintFeatureReference>();
                BuffConfigurator.For(BuffRefs.ArmorFocusLightMythicOffenceSubBuff)
                    .EditComponent<WeaponParametersAttackBonus>(c => dothingy(c, finesseref))
                    .EditComponent<WeaponParametersDamageBonus>(c => dothingy2(c, finesseref))
                    .Configure();
            }
        }

        public static void dothingy(WeaponParametersAttackBonus component, BlueprintFeatureReference refthingy)
        {
            component.CanBeUsedWithFightersFinesse = true;
            component.m_FightersFinesse = refthingy;
        }

        public static void dothingy2(WeaponParametersDamageBonus component, BlueprintFeatureReference refthingy)
        {
            component.CanBeUsedWithFightersFinesse = true;
            component.m_FightersFinesse = refthingy;
        }
    }
}

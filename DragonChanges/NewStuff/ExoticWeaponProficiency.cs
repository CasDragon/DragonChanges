using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Enums;

namespace DragonChanges.NewStuff
{
    internal class ExoticWeaponProficiency
    {
        // edit
        internal const string feature = "ExoticWeaponMaster";
        internal const string featureguid = Guids.ExoticWeaponProficiency;
        internal const string settingName = "exoticweaponproficiency";
        internal const string settingDescription = "Adds a feat that gives all Exotic weapon proficiencies.";
        internal const string featurename = "Exotic Weapon Master";
        internal const string featuredescription = "Allows you to use all exotic weapons.";
        // don't edit
        [DragonLocalizedString(featurenamekey, featurename)]
        internal const string featurenamekey = $"{feature}.name";
        [DragonLocalizedString(featuredescriptionkey, featuredescription)]
        internal const string featuredescriptionkey = $"{feature}.description";
        [DragonConfigure]
        [DragonSetting(SettingCategories.NewAbilities, settingName, settingDescription)]
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
                .AddProficiencies(weaponProficiencies: [WeaponCategory.BastardSword, WeaponCategory.DuelingSword, WeaponCategory.DwarvenWaraxe,
                        WeaponCategory.ElvenCurvedBlade, WeaponCategory.Estoc, WeaponCategory.Falcata, WeaponCategory.Fauchard, WeaponCategory.Kama,
                        WeaponCategory.Sai, WeaponCategory.Tongi, WeaponCategory.SlingStaff, WeaponCategory.DoubleAxe, WeaponCategory.DoubleSword,
                        WeaponCategory.Urgrosh, WeaponCategory.HookedHammer, WeaponCategory.Nunchaku])
                .AddFeatureTagsComponent(featureTags: FeatureTag.Melee | FeatureTag.Ranged)
                .AddPrerequisiteClassLevel(CharacterClassRefs.AnimalCompanionClass.Reference.Get(), level: 1, not: true, hideInUI: true)
                .AddPrerequisiteFeature(FeatureRefs.MartialWeaponProficiency.Reference.Get())
                .AddToGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .SetIsClassFeature(true)
                .Configure();
        }
    }
}

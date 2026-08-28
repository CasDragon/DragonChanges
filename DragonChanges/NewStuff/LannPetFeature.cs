using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;

namespace DragonChanges.NewStuff;

public class LannPetFeature
{
        internal const string feature = "lannpetfeature";
        internal const string settingName = "lannpet";
        internal const string settingDescription = "Meme Option - Adds Lann as a pet for various pet selections, this doesn't make him talk though";
        internal const string featurename = "Animal Companion - Lann";
        internal const string featuredescription = "It's Lann!";
        // 
        [DragonLocalizedString(featurenamekey, featurename)]
        internal const string featurenamekey = "lannpetfeature.name";
        [DragonLocalizedString(featuredescriptionkey, featuredescription, true)]
        internal const string featuredescriptionkey = "lannpetfeature.description";

        [DragonConfigure]
        [DragonSetting(SettingCategories.NewFeatures, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log("Configuring undead mount");
                BlueprintUnit unit = LannPet.ConfigureEnabled();
                BlueprintFeature feat = ConfigureEnabled(unit);
                PetUtils.AddPetToAll(feat);
            }
            else
            {
                Main.log.Log("Undead mount feature disabling, configuring dummies");
                FeatureConfigurator.New(feature, Guids.LannPetFeature)
                    .Configure();
                LannPet.ConfigureDisabled();
            }
        }
        public static BlueprintFeature ConfigureEnabled(BlueprintUnit unit)
        {
            Main.log.Log("Creating undead mount feature");
            return FeatureConfigurator.New(feature, Guids.LannPetFeature)
                .AddPet(pet: unit,
                        type: Kingmaker.Enums.PetType.AnimalCompanion,
                        progressionType: Kingmaker.Enums.PetProgressionType.AnimalCompanion,
                        levelRank: FeatureRefs.AnimalCompanionRank.Reference.Get(),
                        upgradeFeature: FeatureRefs.AnimalCompanionUpgradeHorse.Reference.Get(),
                        upgradeLevel: 4,
                        useContextValueLevel: false,
                        forceAutoLevelup: false,
                        destroyPetOnDeactivate: false)
                .AddPrerequisitePet(noCompanion: true)
                .AddBuffExtraEffects(checkedBuff: BuffRefs.MountedBuff.Reference.Get(),
                        extraEffectBuff: BuffRefs.AnimalCompanionFeatureHorseBuff.Reference.Get(),
                        useBaffContext: false)
                .SetDisplayName(featurenamekey)
                .SetDescription(featuredescriptionkey)
                .SetReapplyOnLevelUp(true)
                .SetIsClassFeature(true)
                .SetRanks(1)
                .Configure();
        }
}
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
namespace DragonChanges.Content
{
    internal class Various
    {
        const string settingName = "hippogriff";
        const string settingDescription = "Adds the Hippogriff animal companion to other pet lists";
        [DragonConfigure]
        [DragonSetting(settingCategories.ModCompatability, settingName, settingDescription)]
        public static void PatchHippogriff()
        {
            if (NewSettings.GetSetting<bool>(settingName))
            {
                Main.log.Log("Patching various animal selections to include Hippogriff");
                BlueprintFeature hippo = FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionBase)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDivineHound)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomain)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomainSeparatist)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDruid)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionHunter)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionMadDog)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionPrimalDruid)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionRanger)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSacredHuntsmaster)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSylvanSorcerer)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionUrbanHunter)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionWildlandShaman)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.CavalierMountSelection)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.BeastRiderMountSelection)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.ArcaneRiderMountSelection)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.BloodriderMountSelection)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.GhostRiderGhostMountSelection)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.NomadMountSelection)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.OrderOfThePawMountSelection)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.PaladinDivineMountSelection)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.SoheiMonasticMountHorseSelection)
                    .AddToAllFeatures(hippo)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.OracleRevelationBondedMount)
                    .AddToAllFeatures(hippo)
                    .Configure();
                if (ModCompat.tttbase)
                {
                    FeatureSelectionConfigurator.For("d9b99d9c48d2425894b565733e96c7e3")
                        .AddToAllFeatures(hippo)
                        .Configure();
                }
            }
        }
        [DragonConfigure]
        public static void CarrierofDisease()
        {
            Main.log.Log("Adding Carrier of Disease to Extra Hex");
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.ExtraWitchHexSelection)
                .AddToAllFeatures(FeatureSelectionRefs.PlagueHexSelection.Reference.Get())
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.ExtraShamanHexSelection)
                .AddToAllFeatures(FeatureSelectionRefs.PlagueHexSelection.Reference.Get())
                .Configure();
        }
        [DragonConfigure]
        public static void ItemStuffs()
        {
            Main.log.Log("Adding Walls of Sanctuary to anevia vendor");
            AneviaVendor.AddItem(ItemShieldRefs.WallsOfTheSanctuaryShieldItem.Reference.Get());
            Main.log.Log("Adding Helmet of Guiding Light to anevia vendor");
            AneviaVendor.AddItem(ItemEquipmentHeadRefs.HelmetOfTheGuidingLight.Reference.Get());
            Main.log.Log("Adding The Priceless Woe to anevia vendor");
            AneviaVendor.AddItem(ItemWeaponRefs.DLC3_NahyndrianVorpalBladeWeaponItem.Reference.Get());
            Main.log.Log("Add Butchers Cleaver (DLC3) to anevia vendor");
            AneviaVendor.AddItem(ItemWeaponRefs.ButcherCleaver_Item.Reference.Get());
        }
    }
}

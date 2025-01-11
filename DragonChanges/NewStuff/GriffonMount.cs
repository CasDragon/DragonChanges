using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using Kingmaker.Modding;
using Kingmaker.View;
using Kingmaker.Visual.Mounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Buffs;

namespace DragonChanges.NewStuff
{
    internal class GriffonMount
    {
        internal static string GriffonUnit = "GriffonMount";
        internal static string GriffonFeatureName = "griffonmountfeature.name";
        internal static string GriffonFeatureDescription = "griffonmountfeature.description";
        internal static string GriffonFeature = "griffonmountfeature";
        internal static string GriffonMountPortrait = "griffonmountportrait";

        public static void Configure()
        {

            if (Settings.GetSetting<bool>("griffonmount"))
            {
                Main.log.Log("Configuring griffon mount");
                BlueprintUnit unit = CreateGriffonMount();
                BlueprintFeature feature = CreateGriffonMountFeature(unit);
                AddGriffonMountToSelections(feature);
            }
            else
            {
                Main.log.Log("Griffon mount feature disabling, configuring dummies");
                FeatureConfigurator.New(GriffonFeature, Guids.GriffonMountFeature)
                    .Configure();
                UnitConfigurator.New(GriffonUnit, Guids.GriffonMountUnit)
                    .Configure();
            }
        }
        public static BlueprintFeature CreateGriffonMountFeature(BlueprintUnit griffonmountunit)
        {
            Main.log.Log("Creating griffon mount feature");
            return FeatureConfigurator.New(GriffonFeature, Guids.GriffonMountFeature)
                .AddPet(pet: griffonmountunit,
                        type: Kingmaker.Enums.PetType.AnimalCompanion,
                        progressionType: Kingmaker.Enums.PetProgressionType.AnimalCompanion,
                        levelRank: FeatureRefs.AnimalCompanionRank.Reference.Get(),
                        upgradeFeature: FeatureRefs.AnimalCompanionUpgradeHorse.Reference.Get(),
                        upgradeLevel: 4)
                .AddPrerequisitePet(noCompanion: true)
                .AddBuffExtraEffects(checkedBuff: BuffRefs.MountedBuff.Reference.Get(),
                        extraEffectBuff: BuffRefs.AnimalCompanionFeatureHorseBuff.Reference.Get(),
                        useBaffContext: false)
                .SetDisplayName(GriffonFeatureName)
                .SetDescription(GriffonFeatureDescription)
                .SetReapplyOnLevelUp(true)
                .SetIsClassFeature(true)
                //.SetIcon("assets/icons/griffonmount.png")
                .AddFeatureToPet(FeatureRefs.MagicalBeastType.Reference.Get())
                .Configure();
        }
        public static BlueprintUnit CreateGriffonMount()
        {
            Main.log.Log("Creating griffon mount unit");
            BlueprintUnit oghorse = TTTHelpers.CreateCopy<BlueprintUnit>(UnitRefs.AnimalCompanionUnitHorse.Reference.Get());
            return UnitConfigurator.New(GriffonUnit, Guids.GriffonMountUnit)
                .CopyFrom(oghorse)
                .SetPrefab(BuffRefs.ShifterWildShapeGriffonBuff.Reference.Get().GetComponent<Polymorph>().m_Prefab)
                .SetType(UnitTypeRefs.EagleGiant.Reference.Get())
                .SetPortrait(BuffRefs.ShifterWildShapeGriffonBuff.Reference.Get().GetComponent<Polymorph>().m_Portrait)
                .Configure();
        }
        public static void AddGriffonMountToSelections(BlueprintFeature mountfeature)
        {
            if (Settings.GetSetting<bool>("griffonmount"))
            {
                Main.log.Log("Patching various animal selections to include griffon mount");
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionBase)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDivineHound)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomain)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomainSeparatist)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDruid)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionHunter)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionMadDog)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionPrimalDruid)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionRanger)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSacredHuntsmaster)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSylvanSorcerer)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionUrbanHunter)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionWildlandShaman)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.CavalierMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.BeastRiderMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.ArcaneRiderMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.BloodriderMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.GhostRiderGhostMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.NomadMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.OrderOfThePawMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.PaladinDivineMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.SoheiMonasticMountHorseSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.OracleRevelationBondedMount)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
            }
        }

    }
}

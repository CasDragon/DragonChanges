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
        internal static string GriffonUnit = "griffonmountunit";
        internal static string GriffonFeatureName = "griffonmountfeature.name";
        internal static string GriffonFeatureDescription = "griffonmountfeature.description";
        internal static string GriffonFeature = "griffonmountfeature";
        internal static string GriffonMountPortrait = "griffonmountportrait";
        readonly static string griffonprefab = BuffRefs.ShifterWildShapeGriffonBuff.Reference.Get().GetComponent<Polymorph>().m_Prefab.AssetId;

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
                .SetIcon(null)
                //.SetIcon("assets/icons/griffonhorse.png")
                .AddFeatureToPet(FeatureRefs.MagicalBeastType.Reference.Get())
                .Configure();
        }
        public static BlueprintUnit CreateGriffonMount()
        {
            Main.log.Log("Creating griffon mount unit");
            BlueprintUnit oghorse = TTTHelpers.CreateCopy<BlueprintUnit>(UnitRefs.AnimalCompanionUnitHorse.Reference.Get());
            //BlueprintPortrait portrait = CreateGriffonMountPortrait();
            return UnitConfigurator.New(GriffonUnit, Guids.GriffonMountUnit)
                .CopyFrom(oghorse)
                .SetPrefab(BuffRefs.ShifterWildShapeGriffonBuff.Reference.Get().GetComponent<Polymorph>().m_Prefab)
                .SetType(UnitTypeRefs.EagleGiant.Reference.Get())
                .SetPortrait(BuffRefs.ShifterWildShapeGriffonBuff.Reference.Get().GetComponent<Polymorph>().m_Portrait)
            //    .SetPortrait(portrait)
                .Configure();
        }
        public static BlueprintPortrait CreateGriffonMountPortrait()
        {
            PortraitData data = new PortraitData();
            // todo: fix this mess
            //data.m_FullLengthImage = "assets/portraits/griffonhorse/fulllength.png";
            //data.m_PetEyeImage = "assets/portraits/griffonhorse/eye.png";
            //data.m_HalfLengthImage = "assets/portraits/griffonhorse/medium.png";
            //data.m_PortraitImage = "assets/portraits/griffonhorse/small.png";
            return PortraitConfigurator.New(GriffonMountPortrait, Guids.GriffonMountPortrait)
                .SetData(data)
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

        //[HarmonyPatch(typeof(OwlcatModificationsManager))]
        [HarmonyPatch(typeof(OwlcatModificationsManager), nameof(OwlcatModificationsManager.OnResourceLoaded))]
        public static class PatchGriffonOnLoad
        {
            /*[HarmonyPostfix]
            [HarmonyPatch(nameof(OwlcatModificationsManager.OnResourceLoaded))]
            public static void OnResourceLoaded_GriffonPatch(object resource, string guid)*/
            [HarmonyPrefix]
            public static void Prefix(object resource, string guid)
            {
                if (guid != griffonprefab)
                    return;
                Main.log.Log("Found griffon prefab");
                if (resource is UnitEntityView view)
                {
                    Main.log.Log("Start patching griffon prefab for mounting");
                    PatchGriffonAsset(view);
                    Main.log.Log("Finised patching griffon prefab");
                }
            }
            public static void PatchGriffonAsset(UnitEntityView view)
            {
                MountOffsets offsets = view.gameObject.AddComponent<MountOffsets>();
                var hypogriff = ResourcesLibrary.TryGetResource<UnitEntityView>(griffonprefab);
                if (hypogriff is UnitEntityView)
                {
                    offsets = UnityEngine.Object.Instantiate(hypogriff.GetComponent<MountOffsets>(), offsets.transform);
                }
                else
                {
                    Main.log.Log("Couldn't find horse prefab to steal from");
                }
            }
        }
    }
}

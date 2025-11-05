using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using static Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite;

namespace DragonChanges.New_Classes.BuildsEnjoyer
{
    internal class Fixes
    {
        public static void DoFixes(BlueprintCharacterClassReference cclass)
        {
            // Sable pet scaling
            ProgressionConfigurator.For(ProgressionRefs.SableMarineAnimalCompanionProgression)
                .AddToClasses([cclass])
                .Configure();
            // Rage powers
            Blueprint<BlueprintReference<BlueprintFeature>>[] powers16 = [FeatureRefs.LethalAccuracyFeature, FeatureRefs.DrunkenKiQuiveringPalmFeature];
            Blueprint<BlueprintReference<BlueprintFeature>>[] powers12 = [FeatureRefs.ComeAndGetMeFeature, FeatureRefs.WreckingBlowsFeature,
                FeatureRefs.FearlessRageFeature, FeatureRefs.DrunkenKiColdIceStrikeFeature, FeatureRefs.DrunkenKiShoutFeature];
            Blueprint<BlueprintReference<BlueprintFeature>>[] powers10 = [FeatureRefs.FiendTotemGreaterFeature, FeatureRefs.DaemonTotemGreaterFeature,
                FeatureRefs.CelestialTotemGreaterFeature, FeatureRefs.BeastTotemGreaterFeature, FeatureRefs.DrunkenKiSpitVenomFeature];
            Blueprint<BlueprintReference<BlueprintFeature>>[] powers8 = [FeatureRefs.ClearMindFeature,
                FeatureRefs.CripplingBlowsFeature, FeatureRefs.ProtectVitalsFeature, FeatureRefs.InternalFortitudeFeature,
                FeatureRefs.DrunkenKiAbudantStepFeature, FeatureRefs.DrunkenKiPoisonFeature, FeatureRefs.DrunkenKiRestorationFeature,
                FeatureRefs.DrunkenKiWholenessOfBodyFeature];
            Blueprint<BlueprintReference<BlueprintFeature>>[] powers6 = [FeatureRefs.ReflexiveDodgeFeature, FeatureRefs.RollingDodgeFeature, 
                FeatureRefs.FiendTotemFeature, FeatureRefs.DaemonTotemFeature, FeatureRefs.CelestialTotemFeature, FeatureRefs.BeastTotemFeature];
            Blueprint<BlueprintReference<BlueprintFeature>>[] powers4 = [FeatureRefs.DeadlyAccuracyFeature, FeatureRefs.RenewedVigorFeature,
                FeatureRefs.DrunkenKiBarskinFeature, FeatureRefs.DrunkenKiScorchingRayFeature, FeatureRefs.DrunkenKiTrueStrikeFeature];
            foreach (var x in powers16)
            {
                RagePowers(cclass, x, 16);
            }
            foreach (var x in powers12)
            {
                RagePowers(cclass, x, 12);
            }
            foreach (var x in powers10)
            {
                RagePowers(cclass, x, 10);
            }
            foreach (var x in powers8)
            {
                RagePowers(cclass, x, 8);
            }
            foreach (var x in powers6)
            {
                RagePowers(cclass, x, 6);
            }
            foreach (var x in powers4)
            {
                RagePowers(cclass, x, 4);
            }
            // Monk
            // Stunning Fist scaling
            var y = AbilityResourceRefs.StunningFistResource.Reference.Get().m_MaxAmount;
            y.m_ClassDiv = [.. y.m_ClassDiv, cclass];
            // Drunken ki scaling
            var z = AbilityResourceRefs.DrunkenKiPowerResource.Reference.Get().m_MaxAmount;
            z.m_ClassDiv = [.. z.m_ClassDiv, cclass];
            // Skald
            // Raging song scaling
            var v = AbilityResourceRefs.RagingSongResource.Reference.Get().m_MaxAmount;
            v.m_Class = [.. v.m_Class, cclass];
        }
        public static void RagePowers(BlueprintCharacterClassReference cclass, Blueprint<BlueprintReference<BlueprintFeature>> feature, int level)
        {
            FeatureConfigurator.For(feature)
                .EditComponents<PrerequisiteClassLevel>(c => c.Group = GroupType.Any, c => true)
                .AddPrerequisiteClassLevel(cclass, level, group: GroupType.Any)
                .Configure();
        }
    }
}

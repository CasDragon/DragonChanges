﻿using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonChanges.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using static TabletopTweaks.Core.MechanicsChanges.MetamagicExtention;

namespace DragonChanges.NewStuff.AutoMetamagics
{
    internal class AutoSolidShadows
    {
        // edit
        internal static string feature = "AutoSolidShadows";
        internal static string featureguid = Guids.AutoSolidShadows;
        // don't edit
        internal static string featurename = $"{feature}.name";
        internal static string featuredescription = $"{feature}.description";
        [DragonConfigure]
        public static void Configure()
        {
            if (Settings.GetSetting<bool>("autometamagics"))
            {
                if (ModCompat.tttbase)
                {
                    Main.log.Log($"{feature} feature enabled, configuring");
                    ConfigureEnabled();
                }
                else
                {
                    Main.log.Log($"TTT-Base wasn't found, configuring dummy for {feature}");
                    ConfigureDummy();
                }
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
            SimpleBlueprint shadow = BlueprintTool.GetRef<BlueprintFeatureReference>("48cba171d3bc4042ae2a18d503816b50").GetBlueprint();
            BlueprintFeature x = FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddRecommendationRequiresSpellbook()
                .AddRecommendationHasFeature(shadow)
                .AddPrerequisiteFeature(shadow)
                .AddToGroups(FeatureGroup.MythicAbility)
                .AddFacts(new() { ConfigureAbility() })
                .Configure();
            FeatureConfigurator.For("48cba171d3bc4042ae2a18d503816b50")
                .AddToIsPrerequisiteFor(x)
                .Configure();
        }
        // edit
        internal static string ability = "AutoSolidShadows-ability";
        internal static string abilityguid = Guids.AutoSolidShadowsAbility;
        // don't edit
        internal static string abilityname = $"{ability}.name";
        internal static string abilitydescription = $"{ability}.description";

        public static BlueprintActivatableAbility ConfigureAbilityDummy()
        {
            return ActivatableAbilityConfigurator.New(ability, abilityguid)
                .Configure();
        }
        public static BlueprintActivatableAbility ConfigureAbility()
        {
            return ActivatableAbilityConfigurator.New(ability, abilityguid)
                .SetDisplayName(abilityname)
                .SetDescription(abilitydescription)
                .SetDeactivateIfCombatEnded(false)
                .SetDeactivateImmediately(true)
                .SetDeactivateIfOwnerUnconscious(true)
                .SetOnlyInCombat(false)
                .SetActivationType(AbilityActivationType.Immediately)
                .SetActivateWithUnitCommand(UnitCommand.CommandType.Swift)
                .SetBuff(ConfigureBuff())
                .SetIcon("Assets/Modifications/DragonChanges 1/AutoSolidShadows.png".ToLower())
                .Configure();
        }
        // edit
        internal static string buff = "AutoSolidShadows-buff";
        internal static string buffguid = Guids.AutoSolidShadowsBuff;
        // don't edit
        internal static string buffname = $"{buff}.name";
        internal static string buffdescription = $"{buff}.description";

        public static BlueprintBuff ConfigureBuffDummy()
        {
            return BuffConfigurator.New(buff, buffguid)
                .Configure();
        }
        public static BlueprintBuff ConfigureBuff()
        {
            return BuffConfigurator.New(buff, buffguid)
                .SetDisplayName(buffname)
                .SetDescription(buffdescription)
                .AddAutoMetamagic(metamagic: (Metamagic)CustomMetamagic.SolidShadows)
                .SetIsClassFeature(true)
                .SetStacking(StackingType.Ignore)
                .SetRanks(0)
                .SetIcon("Assets/Modifications/DragonChanges 1/AutoSolidShadows.png".ToLower())
                .Configure();
        }
    }
}

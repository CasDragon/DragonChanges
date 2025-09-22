using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;

namespace DragonChanges.NewStuff.AutoMetamagics
{
    internal class AutoIntensified
    {
        // edit
        internal const string feature = "AutoIntensified";
        internal const string featureguid = Guids.AutoIntensified;
        // don't edit
        internal const string featurename = $"{feature}.name";
        internal const string featuredescription = $"{feature}.description";
        [DragonConfigure]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>("autometamagics"))
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
            FeatureConfigurator.New(feature, featureguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            BlueprintFeature x = FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddRecommendationRequiresSpellbook()
                .AddRecommendationHasFeature(FeatureRefs.IntensifiedSpell.Reference.Get())
                .AddPrerequisiteFeature(FeatureRefs.IntensifiedSpell.Reference.Get())
                .AddToGroups(FeatureGroup.MythicAbility)
                .AddFacts(new() { ConfigureAbility() })
                .Configure();
            FeatureConfigurator.For(FeatureRefs.IntensifiedSpell)
                .AddToIsPrerequisiteFor(x)
                .Configure();
        }
        // edit
        internal static string ability = "AutoIntensified-ability";
        internal static string abilityguid = Guids.AutoIntensifiedAbility;
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
                .SetIcon(MicroAssetUtil.GetAssemblyResourceSprite("Abilities.AutoIntensified.png"))
                .Configure();
        }
        // edit
        internal static string buff = "AutoIntensified-buff";
        internal static string buffguid = Guids.AutoIntensifiedBuff;
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
                .AddAutoMetamagic(metamagic: Metamagic.Intensified)
                .SetIsClassFeature(true)
                .SetStacking(StackingType.Ignore)
                .SetRanks(0)
                .SetIcon(MicroAssetUtil.GetAssemblyResourceSprite("Abilities.AutoIntensified.png"))
                .Configure();
        }
    }
}

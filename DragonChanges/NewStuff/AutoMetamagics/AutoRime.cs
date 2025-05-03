using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
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
    internal class AutoRime
    {
        // edit
        internal const string feature = "AutoRime";
        internal const string featureguid = Guids.AutoRime;
        // don't edit
        internal const string featurename = $"{feature}.name";
        internal const string featuredescription = $"{feature}.description";
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
            SimpleBlueprint rime = BlueprintTool.GetRef<BlueprintFeatureReference>("192b00c671a64c4c8643f7c18bd1542e").GetBlueprint();
            BlueprintFeature x = FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddRecommendationRequiresSpellbook()
                .AddRecommendationHasFeature(rime)
                .AddPrerequisiteFeature(rime)
                .AddToGroups(FeatureGroup.MythicAbility)
                .AddFacts(new() { ConfigureAbility() })
                .Configure();
            FeatureConfigurator.For("192b00c671a64c4c8643f7c18bd1542e")
                .AddToIsPrerequisiteFor(x)
                .Configure();
        }
        // edit
        internal static string ability = "AutoRime-ability";
        internal static string abilityguid = Guids.AutoRimeAbility;
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
                .SetIcon("Assets/Modifications/DragonChanges 1/AutoRime.png".ToLower())
                .Configure();
        }
        // edit
        internal static string buff = "AutoRime-buff";
        internal static string buffguid = Guids.AutoRimeBuff;
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
                .AddAutoMetamagic(metamagic: (Metamagic)CustomMetamagic.Rime)
                .SetIsClassFeature(true)
                .SetStacking(StackingType.Ignore)
                .SetRanks(0)
                .SetIcon("Assets/Modifications/DragonChanges 1/AutoRime.png".ToLower())
                .Configure();
        }
    }
}

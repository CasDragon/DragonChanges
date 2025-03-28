using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;

namespace DragonChanges.NewStuff.AutoMetamagics
{
    internal class AutoPersistent
    { 
        // edit
        internal static string feature = "AutoPersistent";
        internal static string featureguid = Guids.AutoPersistent;
        // don't edit
        internal static string featurename = $"{feature}.name";
        internal static string featuredescription = $"{feature}.description";
        [DragonConfigure]
        public static void Configure()
        {
            if (Settings.GetSetting<bool>("autometamagics"))
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
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddRecommendationRequiresSpellbook()
                .AddRecommendationHasFeature(FeatureRefs.PersistentSpellFeat.Reference.Get())
                .AddPrerequisiteFeature(FeatureRefs.PersistentSpellFeat.Reference.Get())
                .AddToGroups(FeatureGroup.MythicAbility)
                .AddFacts(new() { ConfigureAbility() })
                .Configure();
        }
        // edit
        internal static string ability = "AutoPersistent-ability";
        internal static string abilityguid = Guids.AutoPersistentAbility;
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
                .SetIcon("Assets/Modifications/DragonChanges 1/AutoPersistent.png".ToLower())
                .Configure();
        }
        // edit
        internal static string buff = "AutoPersistent-buff";
        internal static string buffguid = Guids.AutoPersistentBuff;
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
                .AddAutoMetamagic(metamagic: Metamagic.Persistent)
                .SetIsClassFeature(true)
                .SetStacking(StackingType.Ignore)
                .SetRanks(0)
                .SetIcon("Assets/Modifications/DragonChanges 1/AutoPersistent.png".ToLower())
                .Configure();
        }
    }
}

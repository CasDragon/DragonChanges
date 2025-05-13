using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using DragonChanges.New_Archetypes.Swordmaster_Tengu;
using DragonChanges.NewEnchantments;
using DragonChanges.Patches;
using DragonChanges.Utils;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Commands.Base;

namespace DragonChanges.templates
{
    internal class AbilityTemplate
    {
        // edit
        internal const string ability = "";
        internal const string abilityguid = Guids.Template;
        internal const string settingName = "";
        internal const string settingDescription = "";
        // don't edit
        internal const string abilityname = $"{ability}.name";
        internal const string abilitydescription = $"{ability}.description";
        //[DragonConfigure]
        //[DragonSetting(settingCategories.NewAbilities, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{ability} item enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{ability} disabled, configuring dummy");
                ConfigureDummy();
            }
        }
        public static BlueprintAbility ConfigureDummy()
        {
            return AbilityConfigurator.New(ability, abilityguid)
                .Configure();
        }
        public static BlueprintAbility ConfigureEnabled()
        {
            return AbilityConfigurator.New(ability, abilityguid)
                .SetDisplayName(abilityname)
                .SetDescription(abilitydescription)
                .Configure();
        }
    }
}

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
        internal static string ability = "";
        internal static string abilityguid = Guids.CraneTranceAbility;
        // don't edit
        internal static string abilityname = $"{ability}.name";
        internal static string abilitydescription = $"{ability}.description";
        //[DragonConfigure]
        public static void Configure()
        {
            if (Settings.GetSetting<bool>(ability.ToLower()))
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

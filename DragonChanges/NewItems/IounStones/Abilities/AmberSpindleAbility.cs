
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using DragonChanges.NewItems.IounStones.Buffs;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Commands.Base;

namespace DragonChanges.NewItems.IounStones.Abilities
{
    internal class AmberSpindleAbility
    {
        // edit
        internal const string ability = "AmberSpindleAbility";
        internal const string abilityguid = Guids.AmberSpindleAbility;
        // don't edit
        [DragonLocalizedString(abilityname, "Amber Spindle")]
        internal const string abilityname = $"{ability}.name";
        [DragonLocalizedString(abilitydescription, "This stone grants you a +1 resistance bonus on saving throws.")]
        internal const string abilitydescription = $"{ability}.description";
        public static BlueprintActivatableAbility ConfigureDummy()
        {
            return ActivatableAbilityConfigurator.New(ability, abilityguid)
                .SetDisplayName(abilityname)
                .SetDescription(LocalizedStringHelper.disabledcontentstring)
                .Configure();
        }
        public static BlueprintActivatableAbility ConfigureEnabled()
        {
            return ActivatableAbilityConfigurator.New(ability, abilityguid)
                .SetDisplayName(abilityname)
                .SetDescription(abilitydescription)
                .SetDeactivateIfCombatEnded(false)
                .SetDeactivateImmediately(false)
                .SetDeactivateIfOwnerUnconscious(false)
                .SetOnlyInCombat(false)
                .SetActivationType(AbilityActivationType.Immediately)
                .SetActivateWithUnitCommand(UnitCommand.CommandType.Free)
                .SetBuff(AmberSpindleBuff.ConfigureEnabled())
                //.SetIcon("Assets/Modifications/DragonChanges 1/AutoBolster.png".ToLower())
                .Configure();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using DragonChanges.NewItems.IounStones.Buffs;
using DragonChanges.Utils;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Commands.Base;

namespace DragonChanges.NewItems.IounStones.Abilities
{
    internal class DustyRosePrismAbility
    {
        // edit
        internal const string ability = "DustyRosePrismAbility";
        internal const string abilityguid = Guids.DustyRosePrismAbility;
        // don't edit
        [DragonLocalizedString(abilityname, "Dusty Rose Prism")]
        internal const string abilityname = $"{ability}.name";
        [DragonLocalizedString(abilitydescription, "This stone grants the wearer a +5 insight bonus to AC.")]
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
                .SetBuff(DustyRosePrismBuff.ConfigureEnabled())
                //.SetIcon("Assets/Modifications/DragonChanges 1/AutoBolster.png".ToLower())
                .Configure();
        }
    }
}

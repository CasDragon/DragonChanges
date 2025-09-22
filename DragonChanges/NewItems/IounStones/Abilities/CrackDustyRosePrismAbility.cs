using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using DragonChanges.NewItems.IounStones.Buffs;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Commands.Base;

namespace DragonChanges.NewItems.IounStones.Abilities
{
    internal class CrackDustyRosePrismAbility
    {
        // edit
        internal const string ability = "CrackDustyRosePrismAbility";
        internal const string abilityguid = Guids.CrackDustyRosePrismAbility;
        // don't edit
        [DragonLocalizedString(abilityname, "Cracked Dusty Rose Prism")]
        internal const string abilityname = $"{ability}.name";
        [DragonLocalizedString(abilitydescription, "This stone grants a +1 competence bonus on initiative checks.")]
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
                .SetBuff(CrackDustyRosePrismBuff.ConfigureEnabled())
                //.SetIcon("Assets/Modifications/DragonChanges 1/AutoBolster.png".ToLower())
                .Configure();
        }
    }
}

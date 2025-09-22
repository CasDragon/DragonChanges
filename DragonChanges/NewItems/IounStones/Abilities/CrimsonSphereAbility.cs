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
    internal class CrimsonSphereAbility
    {
        // edit
        internal const string ability = "CrimsonSphereAbility";
        internal const string abilityguid = Guids.CrimsonSphereAbility;
        // don't edit
        [DragonLocalizedString(abilityname, "Crimson Sphere")]
        internal const string abilityname = $"{ability}.name";
        [DragonLocalizedString(abilitydescription, "This stone grants you a +2 enhancement bonus to Intelligence.")]
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
                .SetBuff(CrimsonSphereBuff.ConfigureEnabled())
                //.SetIcon("Assets/Modifications/DragonChanges 1/AutoBolster.png".ToLower())
                .Configure();
        }
    }
}

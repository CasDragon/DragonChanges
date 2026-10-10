using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewSpells.Buffs;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using UnityEngine;

namespace DragonChanges.NewStuff.MythicAbilities;

internal static class MythicFeintAbility
{
    // edit
    internal const string ability = "mythicfeintaoeability";
    internal const string abilityguid = Guids.FeintAoeAbility;

    public static BlueprintAbility ConfigureDummy()
    {
        return AbilityConfigurator.New(ability, abilityguid)
            .SetDisplayName(MythicFeint.featurenamekey)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }

    public static BlueprintAbility ConfigureEnabled()
    {
        var x = AbilityConfigurator.New(ability, abilityguid)
            .SetDisplayName(MythicFeint.featurenamekey)
            .SetDescription(MythicFeint.featuredescriptionkey)
            .SetIcon(MythicFeint.icon)
            .SetType(AbilityType.Physical)
            .SetRange(AbilityRange.Close)
            .SetCanTargetEnemies(true)
            .SetCanTargetFriends(false)
            .SetCanTargetPoint(true)
            .SetCanTargetSelf(false)
            .SetSpellResistance(false)
            .SetEffectOnAlly(AbilityEffectOnUnit.None)
            .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
            .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Point)
            .SetActionType(UnitCommand.CommandType.Swift)
            .AddAbilityEffectRunAction(actions: ActionsBuilder.New()
                .CastSpell(AbilityRefs.FeintAbility.ToString(), false, false, spendAction: true))
            .AddAbilityTargetsAround(targetType: TargetType.Enemy, radius: new Feet(20f), includeDead: false)
            .Configure();
        return x;
    }
}
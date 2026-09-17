using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Visual.Animation.Kingmaker.Actions;

namespace DragonChanges.NewStuff.MythicAbilities.MythicCaster;

public static class MythicCasterAbility
{
    // edit
    internal const string ability = "mythiccasterbaseability";
    internal const string abilityguid = Guids.MythicCasterBaseAbility;

    public static BlueprintAbility ConfigureDummy()
    {
        return AbilityConfigurator.New(ability, abilityguid)
            .SetDisplayName(MythicCasterFeature.featurenamekey)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }

    public static BlueprintAbility ConfigureEnabled(BlueprintAbilityResource resource)
    {
        var x = AbilityConfigurator.New(ability, abilityguid)
            .SetDisplayName(MythicCasterFeature.featurenamekey)
            .SetDescription(MythicCasterFeature.featuredescriptionkey)
            .SetIcon(MythicCasterFeature.icon)
            .AddAbilityEffectRunAction(
                ActionsBuilder.New()
                    .ApplyBuffPermanent(MythicCasterBuff.ConfigureEnabled(MythicCasterFeature.icon),
                        isFromSpell: false, asChild: true, toCaster: true))
            .SetType(AbilityType.Supernatural)
            .SetRange(AbilityRange.Personal)
            .SetCanTargetEnemies(false)
            .SetCanTargetFriends(false)
            .SetCanTargetPoint(false)
            .SetCanTargetSelf(true)
            .SetSpellResistance(false)
            .SetAutoUseIsForbidden(true)
            .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Immediate)
            .SetActionType(UnitCommand.CommandType.Standard)
            .Configure();
        var comp = new AbilityResourceLogic()
        {
            Amount = 1,
            CostIsCustom = false,
            m_IsSpendResource = true,
            m_RequiredResource = resource.ToReference<BlueprintAbilityResourceReference>(),
            ResourceCostDecreasingFacts = [],
            ResourceCostIncreasingFacts = []
        };
        x.Components = [comp, ..x.Components];
        return x;
    }
}
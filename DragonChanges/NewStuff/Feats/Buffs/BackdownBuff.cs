using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using UnityEngine;

namespace DragonChanges.NewStuff.Feats.Buffs;

internal static class BackdownBuff
{
    // edit
    internal const string buff = "BackdownBuff";
    internal const string buffguid = Guids.BackdownBuff;

    public static void ConfigureDummy()
    {
        BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(Backdown.featurenamekey)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }

    public static BlueprintBuff ConfigureEnabled(Sprite icon)
    {
        return BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(Backdown.featurenamekey)
            .SetDescription(Backdown.featuredescriptionkey)
            .SetIcon(icon)
            .SetFlags(BlueprintBuff.Flags.HiddenInUi)
            .AddTargetAttackRollTrigger(onlyHit: true, onlyMelee: true,
                actionsOnAttacker: ActionsBuilder.New()
                    .SavingThrow(SavingThrowType.Will, customDC: ContextValues.CustomProperty(BackdownProperty.ConfigureEnabled(), true),
                        onResult: ActionsBuilder.New()
                            .ConditionalSaved(
                                failed: ActionsBuilder.New()
                                    .ApplyBuff(BackdownFlatfootBuff.ConfigureEnabled().ToReference<BlueprintBuffReference>(),
                                        ContextDuration.Fixed(1)))))
            .Configure();
    }
}
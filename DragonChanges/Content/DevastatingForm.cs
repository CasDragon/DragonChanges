using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Visual.Animation.Kingmaker.Actions;

namespace DragonChanges.Content;

public class DevastatingForm
{
    const string SettingName = "infinitedevastingform";
    const string SettingDescription = "Change Devastating Form from having a duration, to having a permanent buff with an ability to toggle off";
    internal const string abilitynamekey = "dragonweaponfocus";
    internal const string abilitdescriptionkey = "dragonweaponspecial";
    [DragonLocalizedString(abilitynamekey, abilitynamekey)]
    internal const string abilityname = "End Devastating Form";
    [DragonLocalizedString(abilitdescriptionkey, abilitydescription)]
    internal const string abilitydescription = "Ends your Devastating rage!";
    [DragonConfigure]
    [DragonSetting(SettingCategories.ModCompatability, SettingName, SettingDescription, false)]
    public static void MakeInfinite()
    {
        var newAbility = AbilityConfigurator.New(SettingName, Guids.DevastatingFormToggle);
        if (!SettingsAction.GetSetting<bool>(SettingName))
        {
            newAbility
                .SetDisplayName(abilitynamekey)
                .SetDescription(LocalizedStringHelper.disabledcontentstring)
                .Configure();
            return;
        }

        newAbility
            .SetDisplayName(abilitynamekey)
            .SetDescription(abilitdescriptionkey)
            .AddAbilityEffectRunAction(ActionsBuilder.New()
                .RemoveBuff(BuffRefs.RageshaperDevastatingFormBuff.ToString()))
            .SetCanTargetSelf(true)
            .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch)
            .SetActionType(UnitCommand.CommandType.Free)
            .SetRange(AbilityRange.Personal)
            .SetType(AbilityType.Supernatural)
            .Configure();
        var buff = BuffConfigurator.For(BuffRefs.RageshaperDevastatingFormBuff)
            .AddFacts([Guids.DevastatingFormToggle])
            .Configure();
        var ogability = AbilityRefs.RageshaperDevastatingFormAbility.Reference.Get();
        DragonHelpers.RemoveComponent<AbilityEffectRunAction>(ogability);
        AbilityConfigurator.For(ogability)
            .AddAbilityEffectRunAction(
                ActionsBuilder.New()
                    .Conditional(
                        ConditionsBuilder.New()
                            .HasFact(fact: FeatureRefs.MasterShapeshifter.ToString()),
                        ifTrue:
                            ActionsBuilder.New().ApplyBuffPermanentFixed(buff, asChild: true),
                        ifFalse:
                            ActionsBuilder.New().ApplyBuff(buff, ContextDuration.Fixed(1, DurationRate.Minutes, true),
                                asChild: true)))
            .Configure();
    }
}
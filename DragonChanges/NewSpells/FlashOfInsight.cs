using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.NewItems.Scrolls;
using DragonChanges.NewSpells.Buffs;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using UnityEngine;

namespace DragonChanges.NewSpells;

public class FlashOfInsight
{
    // edit
    internal const string ability = "flashofinsight";
    internal const string abilityguid = Guids.FlashOfInsightSpell;
    internal const string settingName = "flashofinsight";
    internal const string settingDescription = "Adds new spell, Flash of Insight. In Paladin spell lists ";
    internal const string iconname = "Abilities.FlashOfInsight.png";
    // don't edit
    internal const string name = "Flash of Insight";
    [DragonLocalizedString(abilityname, name)]
    internal const string abilityname = $"{ability}.name";
    [DragonLocalizedString(abilitydescription, "Peering into the immediate future, you guide your attack with the knowledge of your opponent’s actions. Your next single attack roll (if made before the end of the round in which you cast the spell) gains a +1 insight bonus for every three caster levels you possess (minimum +1).")]
    internal const string abilitydescription = $"{ability}.description";
    internal static readonly Sprite icon = MicroAssetUtil.GetAssemblyResourceSprite(iconname); 

    [DragonConfigure]
    [DragonSetting(SettingCategories.NewSpells, settingName, settingDescription)]
    public static void Configure()
    {
        if (SettingsAction.GetSetting<bool>(settingName))
        {
            Main.log.Log($"{ability} item enabled, configuring");
            ConfigureEnabled();
        }
        else
        {
            Main.log.Log($"{ability} disabled, configuring dummy");
            FlashOfInsightBuff.ConfigureDummy();
            FlashOfInsightScroll.ConfigureDummy();
            ConfigureDummy();
        }
    }
    public static BlueprintAbility ConfigureDummy()
    {
        return AbilityConfigurator.New(ability, abilityguid)
            .SetDisplayName(abilityname)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }

    public static BlueprintAbility ConfigureEnabled()
    {
        var x = AbilityConfigurator.New(ability, abilityguid)
            .SetDisplayName(abilityname)
            .SetDescription(abilitydescription)
            .AddSpellComponent(SpellSchool.Divination)
            .AddAbilityEffectRunAction(
                ActionsBuilder.New()
                    .ApplyBuff(FlashOfInsightBuff.ConfigureEnabled(icon),
                        ContextDuration.Fixed(1, DurationRate.Rounds, true),
                        isFromSpell: true))
            .AddToSpellList(2, SpellListRefs.MagusSpellList.ToString())
            .AddToSpellList(2, SpellListRefs.WizardTransmutationSpellList.ToString())
            .AddToSpellList(2, SpellListRefs.WitchSpellList.ToString())
            .AddToSpellList(2, SpellListRefs.WizardSpellList.ToString())
            .AddToSpellList(2, SpellListRefs.MagicDeceiverSpellList.ToString())
            .SetLocalizedDuration(Duration.OneRound)
            .SetIcon(icon)
            .SetType(AbilityType.Spell)
            .SetRange(AbilityRange.Personal)
            .SetCanTargetEnemies(false)
            .SetCanTargetFriends(false)
            .SetCanTargetPoint(false)
            .SetCanTargetSelf(true)
            .SetSpellResistance(false)
            .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Immediate)
            .SetActionType(UnitCommand.CommandType.Standard)
            .SetAvailableMetamagic(Metamagic.Extend | Metamagic.Heighten | Metamagic.Quicken |
                                   Metamagic.CompletelyNormal)
            .Configure();
        RandomHelpers.AddToThassilonian(x, 2);
        FlashOfInsightScroll.ConfigureEnabled(x, icon);
        return x;
    }
}
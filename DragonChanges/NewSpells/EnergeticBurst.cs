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
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using UnityEngine;

namespace DragonChanges.NewSpells;

public static class EnergeticBurst
{
    // edit
    internal const string ability = "energeticburst";
    internal const string abilityguid = Guids.EnergeticBurstSpell;
    internal const string settingName = "energeticburst";
    internal const string settingDescription = "Adds new spell, Energetic Burst. ";
    internal const string iconname = "Abilities.EnergeticBurst.png";
    // don't edit
    internal const string name = "Energetic Burst";
    [DragonLocalizedString(abilityname, name)]
    internal const string abilityname = $"{ability}.name";
    [DragonLocalizedString(abilitydescription, "The caster gains 10 temporary hit points and a +1 sacred bonus to all saving throws for the spell’s duration.")]
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
            .AddSpellComponent(SpellSchool.Conjuration)
            .AddAbilityEffectRunAction(
                ActionsBuilder.New()
                    .ApplyBuff(FlashOfInsightBuff.ConfigureEnabled(icon),
                        ContextDuration.Variable(ContextValues.Property(UnitProperty.Level, true),
                            DurationRate.Rounds, true),
                        isFromSpell: true))
            .AddToSpellList(2, SpellListRefs.ClericSpellList.ToString())
            .AddToSpellList(2, SpellListRefs.AngelClericSpelllist.ToString())
            .AddToSpellList(2, SpellListRefs.DruidSpellList.ToString())
            .AddToSpellList(1, SpellListRefs.PaladinSpellList.ToString())
            .AddToSpellList(2, SpellListRefs.MagicDeceiverSpellList.ToString())
            .SetLocalizedDuration(Duration.RoundPerLevel)
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
        FlashOfInsightScroll.ConfigureEnabled(x, icon);
        return x;
    }
}
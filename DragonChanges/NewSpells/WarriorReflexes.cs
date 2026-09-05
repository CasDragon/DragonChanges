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
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using UnityEngine;

namespace DragonChanges.NewSpells;

public class WarriorReflexes
{
    // edit
    internal const string ability = "warriorreflexs";
    internal const string abilityguid = Guids.WarriorReflexesSpell;
    internal const string settingName = "warriorreflexs";
    internal const string settingDescription = "Adds a new spell, Warrior Reflexes.";
    internal const string iconname = "Abilities.WarriorsReflexes.png";
    // don't edit
    [DragonLocalizedString(abilityname, "Warrior’s Reflexes")]
    internal const string abilityname = $"{ability}.name";
    [DragonLocalizedString(abilitydescription, "Your allies become aware of what is about to happen in combat. This precognitive awareness grants each creature a +1 insight bonus to AC and Reflex saves.")]
    internal const string abilitydescription = $"{ability}.description";
    internal static readonly Sprite icon = MicroAssetUtil.GetAssemblyResourceSprite(iconname); 
    [DragonConfigure]
    [DragonSetting(SettingCategories.NewSpells, settingName, settingDescription)]
    public static void Configure()
    {
        if (SettingsAction.GetSetting<bool>(settingName))
        {
            Main.log.Log($"{ability} enabled, configuring");
            ConfigureEnabled();
        }
        else
        {
            Main.log.Log($"{ability} disabled, configuring dummy");
            WarriorReflexesBuff.ConfigureDummy();
            ConfigureDummy();
        }
    }
    public static BlueprintAbility ConfigureDummy()
    {
        CommunalEaglesoulScroll.ConfigureDummy();
        return AbilityConfigurator.New(ability, abilityguid)
            .SetDisplayName(abilityname)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }
    public static BlueprintAbility ConfigureEnabled()
    {
        var buff = WarriorReflexesBuff.ConfigureEnabled(icon);
        BlueprintAbility x = AbilityConfigurator.NewSpell(ability, abilityguid, SpellSchool.Divination, false)
            .SetDisplayName(abilityname)
            .SetDescription(abilitydescription)
            .AddAbilityEffectRunAction(
                ActionsBuilder.New()
                    .ApplyBuff(buff,
                        ContextDuration.Variable(ContextValues.Property(UnitProperty.Level), DurationRate.Minutes, true),
                        isFromSpell: true)
                    .PartyMembers(ActionsBuilder.New()
                        .ApplyBuff(buff,
                            ContextDuration.Variable(ContextValues.Property(UnitProperty.Level), DurationRate.Minutes, true),
                            isFromSpell: true)))
            .AddToSpellList(1, SpellListRefs.ClericSpellList.ToString())
            .AddToSpellList(1, SpellListRefs.WizardDivinationSpellList.ToString())
            .AddToSpellList(1, SpellListRefs.MagicDeceiverSpellList.ToString())
            .AddToSpellList(1, SpellListRefs.WizardSpellList.ToString())
            .AddToSpellList(1, SpellListRefs.WarpriestSpelllist.ToString())
            .SetLocalizedDuration(Duration.MinutePerLevel)
            .AddCraftInfoComponent(spellType: Kingmaker.Craft.CraftSpellType.Buff, savingThrow: Kingmaker.Craft.CraftSavingThrow.None, aOEType: Kingmaker.Craft.CraftAOE.AOE)
            .SetIcon(icon)
            .SetType(AbilityType.Spell)
            .SetRange(AbilityRange.Personal)
            .SetCanTargetEnemies(false)
            .SetCanTargetFriends(false)
            .SetCanTargetPoint(false)
            .SetCanTargetSelf(true)
            .SetSpellResistance(false)
            .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Touch)
            .SetActionType(UnitCommand.CommandType.Standard)
            .SetAvailableMetamagic(Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.CompletelyNormal)
            .Configure();
        return x;
    }
}
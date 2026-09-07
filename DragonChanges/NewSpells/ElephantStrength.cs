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

public class ElephantStrength
{
    // edit
    internal const string ability = "elephantstrength";
    internal const string abilityguid = Guids.ElephantStrengthSpell;
    internal const string settingName = "elephantstrength";
    internal const string settingDescription = "Adds new spell, Elephant's Strength. In Paladin spell lists ";
    internal const string iconname = "Abilities.ElephantStrength.png";
    // don't edit
    internal const string name = "Elephant's Strength";
    [DragonLocalizedString(abilityname, name)]
    internal const string abilityname = $"{ability}.name";
    [DragonLocalizedString(abilitydescription, "This spell grants the strength of a fabulous beast. The recipient receives a +6 enhancement bonus to Strength, with all the relevant bonuses that accrue.")]
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
            ElephantStrengthBuff.ConfigureDummy();
            ElephantStrengthScoll.ConfigureDummy();
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
            .AddSpellComponent(SpellSchool.Transmutation)
            .AddAbilityEffectRunAction(
                ActionsBuilder.New()
                    .ApplyBuff(ElephantStrengthBuff.ConfigureEnabled(icon),
                        ContextDuration.Variable(ContextValues.Property(UnitProperty.Level, true),
                            DurationRate.TenMinutes, true),
                        isFromSpell: true))
            .AddToSpellList(4, SpellListRefs.BardSpellList.ToString())
            .AddToSpellList(4, SpellListRefs.ClericSpellList.ToString())
            .AddToSpellList(4, SpellListRefs.AngelClericSpelllist.ToString())
            .AddToSpellList(4, SpellListRefs.WizardSpellList.ToString())
            .AddToSpellList(4, SpellListRefs.LichWizardSpelllist.ToString())
            .AddToSpellList(4, SpellListRefs.WizardTransmutationSpellList.ToString())
            .AddToSpellList(4, SpellListRefs.MagicDeceiverSpellList.ToString())
            .SetLocalizedDuration(Duration.TenMinutesPerLevel)
            .SetIcon(icon)
            .SetType(AbilityType.Spell)
            .SetRange(AbilityRange.Touch)
            .SetCanTargetEnemies(false)
            .SetCanTargetFriends(true)
            .SetCanTargetPoint(false)
            .SetCanTargetSelf(true)
            .SetSpellResistance(true)
            .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Immediate)
            .SetActionType(UnitCommand.CommandType.Standard)
            .SetAvailableMetamagic(Metamagic.Extend | Metamagic.Heighten | Metamagic.Quicken |
                                   Metamagic.CompletelyNormal)
            .Configure();
        ElephantStrengthScoll.ConfigureEnabled(x, icon);
        RandomHelpers.AddToThassilonian(x, 4);
        return x;
    }
}
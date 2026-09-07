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

public class DragonScales
{
    // edit
    internal const string ability = "dragonscales";
    internal const string abilityguid = Guids.DragonScalesSpell;
    internal const string settingName = "dragonscales";
    internal const string settingDescription = "Adds new spell, Dragon Scales. ";
    internal const string iconname = "Abilities.DragonScales.png";
    // don't edit
    internal const string name = "Dragon Scales";
    [DragonLocalizedString(abilityname, name)]
    internal const string abilityname = $"{ability}.name";
    [DragonLocalizedString(abilitydescription, "Your skin hardens, turning the shade and texture of a dragon’s hide, granting +3 natural armor bonus to AC for 8 hours.")]
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
            DragonScalesBuff.ConfigureDummy();
            DragonScaleScroll.ConfigureDummy();
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
                    .ApplyBuff(DragonScalesBuff.ConfigureEnabled(icon),
                        ContextDuration.Fixed(8,
                            DurationRate.Hours, true),
                        isFromSpell: true))
            .AddToSpellList(2, SpellListRefs.WizardTransmutationSpellList.ToString())
            .AddToSpellList(2, SpellListRefs.WizardSpellList.ToString())
            .AddToSpellList(2, SpellListRefs.LichWizardSpelllist.ToString())
            .AddToSpellList(2, SpellListRefs.MagusSpellList.ToString())
            .AddToSpellList(2, SpellListRefs.MagicDeceiverSpellList.ToString())
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
        DragonScaleScroll.ConfigureEnabled(x, icon);
        RandomHelpers.AddToThassilonian(x, 2);
        return x;
    }
}
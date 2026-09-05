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

public class SpiritOfWar
{
    // edit
    internal const string ability = "spiritofwar";
    internal const string abilityguid = Guids.SpiritOfWarSpell;
    internal const string settingName = "spiritofwar";
    internal const string settingDescription = "Adds a new spell, Spirit of War.";
    internal const string iconname = "Abilities.SpiritOfWar.png";
    // don't edit
    [DragonLocalizedString(abilityname, "Spirit of War")]
    internal const string abilityname = $"{ability}.name";
    [DragonLocalizedString(abilitydescription, "You are imbued with the essence of war as divine power courses through your veins.\nWhile under the effects of this spell, you gain the following benefits:\nYour speed increases by 10 feet.\nYou gain a +2 bonus to your AC and Reflex saves")]
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
            SpiritOfWarBuff.ConfigureDummy();
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
        var buff = SpiritOfWarBuff.ConfigureEnabled(icon);
        BlueprintAbility x = AbilityConfigurator.NewSpell(ability, abilityguid, SpellSchool.Conjuration, false)
            .SetDisplayName(abilityname)
            .SetDescription(abilitydescription)
            .AddAbilityEffectRunAction(
                ActionsBuilder.New()
                    .ApplyBuff(buff,
                        ContextDuration.Fixed(1, DurationRate.Minutes, true),
                        isFromSpell: true))
            .AddToSpellList(2, SpellListRefs.ClericSpellList.ToString())
            .AddToSpellList(1, SpellListRefs.PaladinSpellList.ToString())
            .AddToSpellList(2, SpellListRefs.MagicDeceiverSpellList.ToString())
            .AddToSpellList(1, SpellListRefs.WarpriestSpelllist.ToString())
            .SetLocalizedDuration(Duration.OneMinute)
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
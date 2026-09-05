using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.NewSpells.Buffs;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using UnityEngine;

namespace DragonChanges.NewSpells;

public class ChildOfIllFortune
{
    // edit
    internal const string ability = "illfortune";
    internal const string abilityguid = Guids.IllFortuneSpell;
    internal const string settingName = "illfortune";
    internal const string settingDescription = "Adds new spell, Child of Ill Fortune. In Cleric, Oracle, MD, and Angel lists ";
    internal const string iconname = "Abilities.ChildOfIllFortune.png";
    // don't edit
    internal const string name = "Child of Ill Fortune";
    [DragonLocalizedString(abilityname, name)]
    internal const string abilityname = $"{ability}.name";
    [DragonLocalizedString(abilitydescription, "For the spell’s duration, the target suffers -2 penalties to all attack, damage (weapon and spell), skill check and ability check rolls. Any spells cast by the target have their DC decreased by 5. Child of ill fortune lasts a full 24 hours unless removed by dispel magic or remove curse.")]
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
            ChildOfIllFortune.ConfigureDummy();
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
            .AddSpellDescriptorComponent(SpellDescriptor.Curse)
            .AddAbilityEffectRunAction(
                ActionsBuilder.New()
                    .SavingThrow(
                        SavingThrowType.Will,
                        onResult:
                            new ActionsBuilder()
                                .ConditionalSaved(
                                    failed: new ActionsBuilder()
                                        .ApplyBuff(IllFortuneBuff.ConfigureEnabled(icon),
                                            ContextDuration.Fixed(24, DurationRate.Hours, false),
                                        isFromSpell: true))))
            .AddToSpellList(6, SpellListRefs.ClericSpellList.ToString())
            .AddToSpellList(6, SpellListRefs.MagicDeceiverSpellList.ToString())
            .AddToSpellList(6, SpellListRefs.AngelMythicSpelllist.ToString())
            .AddCraftInfoComponent(spellType: Kingmaker.Craft.CraftSpellType.Debuff, savingThrow: Kingmaker.Craft.CraftSavingThrow.Will, aOEType: Kingmaker.Craft.CraftAOE.None)
            .SetLocalizedDuration(Duration.TwentyFourHours)
            .SetIcon(icon)
            .SetType(AbilityType.Spell)
            .SetRange(AbilityRange.Close)
            .SetCanTargetEnemies(true)
            .SetCanTargetFriends(true)
            .SetCanTargetPoint(false)
            .SetCanTargetSelf(false)
            .SetSpellResistance(true)
            .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Point)
            .SetActionType(UnitCommand.CommandType.Standard)
            .SetAvailableMetamagic(Metamagic.Heighten | Metamagic.Quicken | Metamagic.CompletelyNormal)
            .Configure();
        return x;
    }
}
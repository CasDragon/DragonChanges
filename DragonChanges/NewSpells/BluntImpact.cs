using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Utils.Types;
using DragonChanges.NewItems.Scrolls;
using DragonChanges.NewSpells.Buffs;
using DragonChanges.Utils;
using DragonLibrary.BPCoreExtensions;
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

public static class BluntImpact
{
    // edit
    internal const string ability = "bluntimpact";
    internal const string abilityguid = Guids.BluntImpactSpell;
    internal const string settingName = "bluntimpact";
    internal const string settingDescription = "Adds a new spell, Blunt Impact.";
    internal const string iconname = "Abilities.BluntImpact.png";
    // don't edit
    [DragonLocalizedString(abilityname, "Blunt Impact")]
    internal const string abilityname = $"{ability}.name";
    [DragonLocalizedString(abilitydescription, "This {g|Encyclopedia:Spell}spell{/g} makes a weapon magically keen, improving its ability to deal telling blows. This transmutation doubles the {g|Encyclopedia:Critical}threat range{/g} of the weapon. A threat range of 20 becomes 19–20, a threat range of 19–20 becomes 17–20, and a threat range of 18–20 becomes 15–20. The spell can be cast only on {g|Encyclopedia:Damage_Type}bludgeoning{/g} weapons. Multiple effects that increase a weapon's threat range (such as the keen special weapon property and the Improved Critical {g|Encyclopedia:Feat}feat{/g}) don't stack. You can't cast this spell on a {g|Encyclopedia:NaturalAttack}natural weapon{/g}, such as a claw.")]
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
            BluntImpactBuff.ConfigureDummy();
            BluntImpactScroll.ConfigureDummy();
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
        var buff = BluntImpactBuff.ConfigureEnabled(icon);
        BlueprintAbility x = AbilityConfigurator.NewSpell(ability, abilityguid, SpellSchool.Transmutation, false)
            .SetDisplayName(abilityname)
            .SetDescription(abilitydescription)
            .AddAbilityEffectRunAction(
                ActionsBuilder.New()
                    .Conditional(new ConditionsBuilder().
                            IsWeaponCategoryGroupEquipped(WeaponGroupCategory.Blunt),
                        ifTrue:
                        ActionsBuilder.New()
                            .ApplyBuff(buff,
                                ContextDuration.Variable(ContextValues.Property(UnitProperty.Level), DurationRate.TenMinutes, true),
                                isFromSpell: true)
                            .EnhanceWeapon(
                                ContextDuration.Variable(ContextValues.Property(UnitProperty.Level), DurationRate.TenMinutes, true),
                                WeaponEnchantmentRefs.Keen.ToString()))
                    .Conditional(new ConditionsBuilder().
                            IsWeaponCategoryGroupEquipped(WeaponGroupCategory.Blunt, true, false),
                        ifTrue:
                        ActionsBuilder.New()
                            .ApplyBuff(buff,
                                ContextDuration.Variable(ContextValues.Property(UnitProperty.Level), DurationRate.TenMinutes, true),
                                isFromSpell: true)
                            .EnhanceWeapon(
                                ContextDuration.Variable(ContextValues.Property(UnitProperty.Level), DurationRate.TenMinutes, true),
                                WeaponEnchantmentRefs.Keen.ToString())))
            .AddToSpellList(3, SpellListRefs.InquisitorSpellList.ToString())
            .AddToSpellList(3, SpellListRefs.WizardTransmutationSpellList.ToString())
            .AddToSpellList(3, SpellListRefs.LichWizardSpelllist.ToString())
            .AddToSpellList(3, SpellListRefs.MagicDeceiverSpellList.ToString())
            .AddToSpellList(3, SpellListRefs.WizardSpellList.ToString())
            .AddToSpellList(3, SpellListRefs.MagusSpellList.ToString())
            .SetLocalizedDuration(Duration.TenMinutesPerLevel)
            .AddCraftInfoComponent(spellType: Kingmaker.Craft.CraftSpellType.Buff, savingThrow: Kingmaker.Craft.CraftSavingThrow.None, aOEType: Kingmaker.Craft.CraftAOE.AOE)
            .SetIcon(icon)
            .SetType(AbilityType.Spell)
            .SetRange(AbilityRange.Touch)
            .SetCanTargetEnemies(false)
            .SetCanTargetFriends(true)
            .SetCanTargetPoint(false)
            .SetCanTargetSelf(true)
            .SetSpellResistance(false)
            .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Touch)
            .SetActionType(UnitCommand.CommandType.Standard)
            .SetAvailableMetamagic(Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.CompletelyNormal)
            .Configure();
        BluntImpactScroll.ConfigureEnabled(x, icon);
        RandomHelpers.AddToThassilonian(x, 3);
        return x;
    }
}
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
using Kingmaker.UI.Common;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using UnityEngine;

namespace DragonChanges.NewSpells;

public class BloodSlugs
{
    // edit
    internal const string ability = "bloodslugs";
    internal const string abilityguid = Guids.BloodSlugsSpell;
    internal const string settingName = "bloodslugs";
    internal const string settingDescription = "Adds new spell, Blood Slugs. In Druid, MD, and Demon lists ";
    internal const string iconname = "Abilities.BloodSlugs.png";
    // don't edit
    internal const string name = "Blood Slugs";
    [DragonLocalizedString(abilityname, name)]
    internal const string abilityname = $"{ability}.name";
    [DragonLocalizedString(abilitydescription, "You create blood-thirsty slugs that burrow into flesh and settle in veins. The slugs appear on the subject’s body and immediately attempt to penetrate their skin. Targets must make a Fortitude save for each blood slug affecting them. Each failed saving throw deals 1 point of Constitution damage and reduces the target’s speed by 5 feet (minimum 5). The damage from multiple blood slugs stacks.")]
    internal const string abilitydescription = $"{ability}.description";
    [DragonConfigure]
    [DragonSetting(SettingCategories.NewSpells, settingName, settingDescription)]
    public static void Configure()
    {
        if (SettingsAction.GetSetting<bool>(settingName))
        {
            Main.log.Log($"{ability} item enabled, configuring");
            var icon = MicroAssetUtil.GetAssemblyResourceSprite(iconname); 
            ConfigureEnabled(icon);
        }
        else
        {
            Main.log.Log($"{ability} disabled, configuring dummy");
            BloodSlugsBuff.ConfigureDummy();
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
    public static BlueprintAbility ConfigureEnabled(Sprite icon)
    {
        return AbilityConfigurator.New(ability, abilityguid)
            .SetDisplayName(abilityname)
            .SetDescription(abilitydescription)
                .AddSpellComponent(SpellSchool.Conjuration)
                .AddAbilityEffectRunAction(
                    ActionsBuilder.New()
                        .SavingThrow(
                            SavingThrowType.Fortitude,
                            onResult:
                                new ActionsBuilder()
                                    .ConditionalSaved(
                                        failed: new ActionsBuilder()
                                            .ApplyBuff(BloodSlugsBuff.ConfigureEnabled(icon),
                                                ContextDuration.Variable(ContextValues.Property(UnitProperty.Level, true),
                                                DurationRate.Rounds, true),
                                            isFromSpell: true))))
                .AddToSpellList(4, SpellListRefs.DruidSpellList.Reference.Get())
                .AddToSpellList(4, SpellListRefs.MagicDeceiverSpellList.Reference.Get())
                .AddToSpellList(4, SpellListRefs.DemonSpelllist.Reference.Get())
                .AddCraftInfoComponent(spellType: Kingmaker.Craft.CraftSpellType.Debuff, savingThrow: Kingmaker.Craft.CraftSavingThrow.Fortitude, aOEType: Kingmaker.Craft.CraftAOE.None)
                .SetLocalizedDuration(Duration.RoundPerLevel)
                .SetIcon(icon)
                .SetType(AbilityType.Spell)
                .SetRange(AbilityRange.Close)
                .SetCanTargetEnemies(true)
                .SetCanTargetFriends(false)
                .SetCanTargetPoint(false)
                .SetCanTargetSelf(false)
                .SetSpellResistance(true)
                .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Point)
                .SetActionType(UnitCommand.CommandType.Standard)
                .SetAvailableMetamagic(Metamagic.Extend | Metamagic.Heighten | Metamagic.Quicken | Metamagic.CompletelyNormal)
            .Configure();
    }
}
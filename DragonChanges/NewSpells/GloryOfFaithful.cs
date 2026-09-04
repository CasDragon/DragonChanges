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
using UnityEngine.UI;

namespace DragonChanges.NewSpells;

public class GloryOfFaithful
{
    // edit
    internal const string ability = "gloryoffaithful";
    internal const string abilityguid = Guids.GloryOfFaithSpell;
    internal const string settingName = "gloryofaithful";
    internal const string settingDescription = "Adds new spell, Glory of the Faithful. In Paladin spell lists ";
    internal const string iconname = "Abilities.GloryOfTheFaithful.png";
    // don't edit
    internal const string name = "Glory of the Faithful";
    [DragonLocalizedString(abilityname, name)]
    internal const string abilityname = $"{ability}.name";
    [DragonLocalizedString(abilitydescription, "You are infused with the power and glory of all those faithful to your god, both living and dead. You gain a divine bonus to attack, damage, and AC equal to your caster level.")]
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
            GloryOfFaithfulBuff.ConfigureDummy();
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
            .AddSpellComponent(SpellSchool.Transmutation)
            .AddAbilityEffectRunAction(
                ActionsBuilder.New()
                    .ApplyBuff(GloryOfFaithfulBuff.ConfigureEnabled(icon),
                        ContextDuration.Variable(ContextValues.Property(UnitProperty.Level, true),
                            DurationRate.Rounds, true),
                        isFromSpell: true))
            .AddToSpellList(4, SpellListRefs.PaladinSpellList.Reference.Get())
            .SetLocalizedDuration(Duration.RoundPerLevel)
            .SetIcon(icon)
            .SetType(AbilityType.Spell)
            .SetRange(AbilityRange.Personal)
            .SetCanTargetEnemies(false)
            .SetCanTargetFriends(false)
            .SetCanTargetPoint(false)
            .SetCanTargetSelf(true)
            .SetSpellResistance(true)
            .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Immediate)
            .SetActionType(UnitCommand.CommandType.Standard)
            .SetAvailableMetamagic(Metamagic.Extend | Metamagic.Heighten | Metamagic.Quicken |
                                   Metamagic.CompletelyNormal)
            .Configure();
    }
}
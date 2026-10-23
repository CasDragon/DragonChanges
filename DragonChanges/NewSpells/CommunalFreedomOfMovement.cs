using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.NewSpells.Buffs;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using UnityEngine;

namespace DragonChanges.NewSpells;

internal static class CommunalFreedomOfMovement
{
    // edit
    internal const string ability = "communalfom";
    internal const string abilityguid = Guids.FreedomOfMovementCommunal;
    internal const string settingName = "communalfom";
    internal const string settingDescription = "Adds new spell, communal version of Freedom of Movement";
    
    internal const string name = "Freedom of Movement, Communal";

    internal const string description = "This {g|Encyclopedia:Spell}spell{/g} functions like Freedom of Movement, except it affects all party members and it lasts for 4 hours.";

    // don't edit
    [DragonLocalizedString(abilityname, name)]
    internal const string abilityname = $"{ability}.name";

    [DragonLocalizedString(abilitydescription, description)]
    internal const string abilitydescription = $"{ability}.description";

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

    public static BlueprintAbility ConfigureEnabled()
    {
        var x = AbilityConfigurator.New(ability, abilityguid)
            .SetDisplayName(abilityname)
            .SetDescription(abilitydescription)
            .AddSpellComponent(SpellSchool.Abjuration)
            .AddAbilitySpawnFx(AbilitySpawnFxAnchor.ClickedTarget, 0, false,
                AbilitySpawnFxAnchor.None, AbilitySpawnFxOrientation.Copy, AbilitySpawnFxAnchor.None, 
                "7a401ecd81c12364c92b7b6551031cdb", AbilitySpawnFxTime.OnApplyEffect, AbilitySpawnFxWeaponTarget.None)
            .AddAbilityTargetsAround(includeDead: false, targetType: TargetType.Ally, radius: new Feet(30), spreadSpeed: new Feet(15))
            .AddAbilityEffectRunAction(
                ActionsBuilder.New()
                    .ApplyBuff(BuffRefs.FreedomOfMovementBuff.ToString(),
                        ContextDuration.Fixed(4, DurationRate.Hours, true),
                        isFromSpell: true))
            .AddToSpellList(7, SpellListRefs.ClericSpellList.ToString())
            .AddToSpellList(7, SpellListRefs.AeonSpellList.ToString())
            .AddToSpellList(7, SpellListRefs.BardSpellList.ToString())
            .AddToSpellList(7, SpellListRefs.DruidSpellList.ToString())
            .AddToSpellList(7, SpellListRefs.AngelClericSpelllist.ToString())
            .AddToSpellList(7, SpellListRefs.AzataMythicSpelllist.ToString())
            .AddToSpellList(7, SpellListRefs.MagicDeceiverSpellList.ToString())
            .AddCraftInfoComponent(spellType: CraftSpellType.Buff,
                savingThrow: CraftSavingThrow.None, aOEType: CraftAOE.AOE)
            .SetIcon(AbilityRefs.FreedomOfMovementCast.Reference.Get().Icon)
            .SetType(AbilityType.Spell)
            .SetRange(AbilityRange.Personal)
            .SetCanTargetEnemies(false)
            .SetCanTargetFriends(true)
            .SetCanTargetPoint(true)
            .SetCanTargetSelf(true)
            .SetSpellResistance(false)
            .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Point)
            .SetActionType(UnitCommand.CommandType.Standard)
            .SetAvailableMetamagic(Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal)
            .Configure();
        return x;
    }
}
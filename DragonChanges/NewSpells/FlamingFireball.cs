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
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using TabletopTweaks.Core.MechanicsChanges;
using UnityEngine;

namespace DragonChanges.NewSpells;

internal static class FlamingFireball
{
    // edit
    internal const string ability = "FlamingFireball";
    internal const string abilityguid = Guids.FlamingFireball;
    internal const string settingName = "flamingfireball";
    internal const string settingDescription = "Adds a new spell, Flaming Fireball";
    //internal const string iconname = "Abilities.spell.png";
    internal const string name = "Flaming Fireball";

    internal const string description = "A fireball {g|Encyclopedia:Spell}spell{/g} generates a searing explosion of flame that detonates with a low roar and deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} per {g|Encyclopedia:Caster_Level}caster level{/g} (no maximum) to every creature within the area. A successful {g|Encyclopedia:Saving_Throw}Reflex save{/g} halves this damage.";

    // don't edit
    [DragonLocalizedString(abilityname, name)]
    internal const string abilityname = $"{ability}.name";

    [DragonLocalizedString(abilitydescription, description)]
    internal const string abilitydescription = $"{ability}.description";

    //internal static readonly Sprite icon = MicroAssetUtil.GetAssemblyResourceSprite(iconname);

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
        var meta = Metamagic.Heighten | Metamagic.Quicken | Metamagic.CompletelyNormal | Metamagic.Bolstered |
                   Metamagic.Maximize | Metamagic.Persistent | Metamagic.Selective | Metamagic.Piercing |
                   Metamagic.Intensified;
        if (ModCompat.tttbase)
            meta = meta | (Metamagic)(MetamagicExtention.CustomMetamagic.Burning |
                                      MetamagicExtention.CustomMetamagic.ElementalAcid |
                                      MetamagicExtention.CustomMetamagic.ElementalCold |
                                      MetamagicExtention.CustomMetamagic.ElementalElectricity |
                                      MetamagicExtention.CustomMetamagic.ElementalFire |
                                      MetamagicExtention.CustomMetamagic.Flaring);
        var x = AbilityConfigurator.New(ability, abilityguid)
            .SetDisplayName(abilityname)
            .SetDescription(abilitydescription)
            .AddSpellComponent(SpellSchool.Evocation)
            .AddToSpellList(6, SpellListRefs.WizardEvocationSpellList.ToString())
            .AddToSpellList(6, SpellListRefs.WizardSpellList.ToString())
            .AddToSpellList(6, SpellListRefs.LichWizardSpelllist.ToString())
            .AddToSpellList(6, SpellListRefs.FireDomainSpellList.ToString())
            .AddToSpellList(6, SpellListRefs.MagusSpellList.ToString())
            .AddToSpellList(6, SpellListRefs.MagicDeceiverSpellList.ToString())
            .AddToSpellList(6, SpellListRefs.BloodragerSpellList.ToString())
            .AddToSpellList(6, SpellListRefs.FlamesSpiritSpellList.ToString())
            .AddToSpellList(6, SpellListRefs.DemonUsualSpelllist.ToString())
            .AddAbilityDeliverProjectile(type: AbilityProjectileType.Simple,
                useCastPositionInsteadCasterPosition: false,
                projectiles: [ProjectileRefs.Fireball00.ToString()],
                needAttackRoll: false,
                useMaxProjectilesCount: false)
            .AddAbilityEffectRunAction(ActionsBuilder.New()
                .DealDamage(damageType: new DamageTypeDescription()
                {
                    Type = DamageType.Energy,
                    Common = new DamageTypeDescription.CommomData(),
                    Physical = new DamageTypeDescription.PhysicalData(),
                    Energy = DamageEnergyType.Fire
                },
                    value: ContextDice.Value(DiceType.D6, ContextValues.Rank()),
                    isAoE: true,
                    halfIfSaved: true),
                savingThrowType: SavingThrowType.Reflex)
            .AddAbilityTargetsAround(radius: new Feet(20), spreadSpeed: new Feet(25), includeDead: false, targetType: TargetType.Any)
            .AddContextRankConfig(ContextRankConfigs.CasterLevel())
            .AddSpellDescriptorComponent(SpellDescriptor.Fire)
            .AddCraftInfoComponent(spellType: CraftSpellType.Damage,
                savingThrow: CraftSavingThrow.Reflex, aOEType: CraftAOE.AOE)
            .SetIcon(AbilityRefs.Fireball.Reference.Get().Icon)
            .SetType(AbilityType.Spell)
            .SetRange(AbilityRange.Long)
            .SetCanTargetEnemies(true)
            .SetCanTargetFriends(true)
            .SetCanTargetPoint(true)
            .SetCanTargetSelf(true)
            .SetSpellResistance(true)
            .SetEffectOnAlly(AbilityEffectOnUnit.Harmful)
            .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
            .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Directional)
            .SetActionType(UnitCommand.CommandType.Standard)
            .SetAvailableMetamagic(meta)
            .Configure();
        return x;
    }
}
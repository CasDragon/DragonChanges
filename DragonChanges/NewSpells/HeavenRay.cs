﻿using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using Kingmaker.Enums;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Utility;
using BlueprintCore.Actions.Builder;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.RuleSystem;
using BlueprintCore.Actions.Builder.BasicEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Enums.Damage;
using BlueprintCore.Actions.Builder.ContextEx;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.View.Animation;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;
using System.Linq;
using static TabletopTweaks.Core.MechanicsChanges.MetamagicExtention;
using Kingmaker.UI.SettingsUI;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonChanges.NewSpells
{
    internal class HeavenRay
    {
        // edit
        internal const string spell = "HeavenRay";
        internal const string spellguid = Guids.HeavenRay;
        internal const string settingName = "heavenray";
        internal const string settingDescription = "Enable the Heavenfire Ray spell";
        // don't edit
        internal const string spellname = $"{spell}.name";
        internal const string spelldescription = $"{spell}.description";
        [DragonConfigure]
        [DragonSetting(settingCategories.NewSpells, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{spell} feature enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{spell} disabled, configuring dummy");
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            AbilityConfigurator.New(spell, spellguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            BlueprintAbility hellfire = AbilityRefs.HellfireRay.Reference.Get();
            Metamagic metas = hellfire.AvailableMetamagic;
            if (ModCompat.tttbase)
            {
                metas = metas | (Metamagic) (CustomMetamagic.Burning | CustomMetamagic.ElementalAcid |
                    CustomMetamagic.ElementalCold | CustomMetamagic.ElementalElectricity |
                    CustomMetamagic.ElementalFire | CustomMetamagic.Flaring);
            }
            ContextRankConfig crc1 = TTTHelpers.CreateCopy(hellfire.GetComponent<ContextRankConfig>(c => c.Type == AbilityRankType.ProjectilesCount));
            ContextRankConfig crc2 = TTTHelpers.CreateCopy(hellfire.GetComponent<ContextRankConfig>(c => c.Type == AbilityRankType.Default));
            AbilityConfigurator.NewSpell(spell, spellguid, SpellSchool.Evocation, true, SpellDescriptor.Fire | SpellDescriptor.Good)
                .SetDisplayName(spellname)
                .SetDescription(spelldescription)
                // components
                .AddToSpellList(level: 6, spellList: SpellListRefs.WizardSpellList.Reference.Get())
                .AddToSpellList(level: 6, spellList: SpellListRefs.MagusSpellList.Reference.Get())
                .AddToSpellList(level: 6, spellList: SpellListRefs.ClericSpellList.Reference.Get())
                .AddToSpellList(level: 6, spellList: SpellListRefs.WitchSpellList.Reference.Get())
                .AddToSpellList(level: 6, spellList: SpellListRefs.MagicDeceiverSpellList.Reference.Get())
                .AddAbilityDeliverProjectile(
                        projectiles: [ProjectileRefs.PolarRay00.Reference.Get(),
                                ProjectileRefs.RayOfFrost00.Reference.Get(),
                                ProjectileRefs.PolarRay00.Reference.Get()],
                        type: Kingmaker.UnitLogic.Abilities.Components.AbilityProjectileType.Simple,
                        needAttackRoll: true,
                        weapon: ItemWeaponRefs.RayItem.Reference.Get(),
                        replaceAttackRollBonusStat: false,
                        useMaxProjectilesCount: true,
                        maxProjectilesCountRank: AbilityRankType.ProjectilesCount,
                        delayBetweenProjectiles: 0.4f)
                .AddAbilityEffectRunAction(
                        actions: ActionsBuilder.New()
                                    .DealDamage(
                                        DamageTypes.Energy(DamageEnergyType.Fire),
                                        ContextDice.Value(DiceType.D6, ContextValues.Rank(AbilityRankType.Default)),
                                        half: true)
                                    .DealDamage(
                                        DamageTypes.Energy(DamageEnergyType.Holy),
                                        ContextDice.Value(DiceType.D6, ContextValues.Rank(AbilityRankType.Default)),
                                        half: true
                                        )
                                    .Add(new ContextActionDisableBonusForDamage()
                                    {
                                        DisableAdditionalDamage = true,
                                        DisableAdditionalDice = false,
                                        DisableFavoredEnemyDamage = true,
                                        DisableSneak = true
                                    }))
                .AddContextRankConfig(crc1)
                .AddContextRankConfig(crc2)
                // fields
                .SetType(AbilityType.Spell)
                .SetRange(AbilityRange.Close)
                .SetCanTargetEnemies(true)
                .SetCanTargetSelf(true)
                .SetShouldTurnToTarget(true)
                .SetSpellResistance(true)
                .SetEffectOnAlly(AbilityEffectOnUnit.None)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Directional)
                .SetActionType(CommandType.Standard)
                .SetAvailableMetamagic(metas)
                .AddCraftInfoComponent(
                    aOEType: Kingmaker.Craft.CraftAOE.None,
                    savingThrow: Kingmaker.Craft.CraftSavingThrow.None,
                    spellType: Kingmaker.Craft.CraftSpellType.Damage)
                .SetIcon("Assets/Modifications/DragonChanges 1/HeavenFireRay.png".ToLower())
                .Configure();
        }
    }
}

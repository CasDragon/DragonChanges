using BlueprintCore.Blueprints.CustomConfigurators.Classes;
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
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using DragonChanges.New_Components;
using Kingmaker.UnitLogic.Abilities.Components;

namespace DragonChanges.NewSpells
{
    internal class Hellball
    {
        // edit
        internal static string spell = "HellBall";
        internal static string spellguid = Guids.HellBall;
        // don't edit
        internal static string spellname = $"{spell}.name";
        internal static string spelldescription = $"{spell}.description";

        [DragonConfigure]
        public static void Configure()
        {
            if (Settings.GetSetting<bool>(spell.ToLower()))
            {
                Main.log.Log($"{spell} feature enabled, configuring");
                BlueprintAbility newspell = ConfigureEnabled();
                ConfigureFeatureEnabled(newspell);
            }
            else
            {
                Main.log.Log($"{spell} disabled, configuring dummy");
                ConfigureDummy();
                ConfigureFeatureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            AbilityConfigurator.New(spell, spellguid).Configure();
        }
        public static BlueprintAbility ConfigureEnabled()
        {
            Metamagic metas = AbilityRefs.HellfireRay.Reference.Get().AvailableMetamagic;
            if (ModCompat.tttbase)
            {
                metas = metas | (Metamagic)(CustomMetamagic.Burning | CustomMetamagic.ElementalAcid |
                    CustomMetamagic.ElementalCold | CustomMetamagic.ElementalElectricity |
                    CustomMetamagic.ElementalFire | CustomMetamagic.Flaring | CustomMetamagic.Rime);
            }
            string spellicon = "Assets/Modifications/DragonChanges 1/HellBall.png".ToLower();
            if (Settings.GetSetting<bool>("hellballicon"))
            {
                spellicon = "Assets/Modifications/DragonChanges 1/HellBallWrong.png".ToLower();
            }
            return AbilityConfigurator.NewSpell(spell, spellguid, SpellSchool.Evocation, true, SpellDescriptor.Fire | SpellDescriptor.Acid | SpellDescriptor.Sonic | SpellDescriptor.Electricity)
                .SetDisplayName(spellname)
                .SetDescription(spelldescription)
                // components
                .AddAbilityDeliverProjectile(
                        projectiles: [ProjectileRefs.Fireball00.Reference.Get()],
                        type: Kingmaker.UnitLogic.Abilities.Components.AbilityProjectileType.Simple,
                        needAttackRoll: false)
                .AddAbilityEffectRunAction(
                        savingThrowType: Kingmaker.EntitySystem.Stats.SavingThrowType.Reflex,
                        actions: ActionsBuilder.New()
                                    .DealDamage(
                                        DamageTypes.Energy(DamageEnergyType.Fire),
                                        ContextDice.Value(DiceType.D6, ContextValues.Constant(20)),
                                        isAoE: true,
                                        halfIfSaved: true)
                                    .DealDamage(
                                        DamageTypes.Energy(DamageEnergyType.Sonic),
                                        ContextDice.Value(DiceType.D6, ContextValues.Constant(20)),
                                        isAoE: true,
                                        halfIfSaved: true)
                                    .DealDamage(
                                        DamageTypes.Energy(DamageEnergyType.Electricity),
                                        ContextDice.Value(DiceType.D6, ContextValues.Constant(20)),
                                        isAoE: true,
                                        halfIfSaved: true)
                                    .DealDamage(
                                        DamageTypes.Energy(DamageEnergyType.Acid),
                                        ContextDice.Value(DiceType.D6, ContextValues.Constant(20)),
                                        isAoE: true,
                                        halfIfSaved: true)
                                    .Add(new ContextActionDisableBonusForDamage()
                                    {
                                        DisableAdditionalDamage = false,
                                        DisableAdditionalDice = false,
                                        DisableFavoredEnemyDamage = true,
                                        DisableSneak = true
                                    }))
                .AddAbilityTargetsAround(radius: new Feet(30), spreadSpeed: new Feet(25), includeDead: false, targetType: TargetType.Any)
                // fields
                .SetType(AbilityType.Spell)
                .SetRange(AbilityRange.Long)
                .SetCanTargetPoint(true)
                .SetCanTargetEnemies(true)
                .SetCanTargetFriends(true)
                .SetCanTargetSelf(true)
                .SetShouldTurnToTarget(true)
                .SetSpellResistance(true)
                .SetIgnoreSpellResistanceForAlly(false)
                .SetEffectOnAlly(AbilityEffectOnUnit.Harmful)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Directional)
                .SetActionType(CommandType.Standard)
                .SetAvailableMetamagic(metas)
                .AddCraftInfoComponent(
                    aOEType: Kingmaker.Craft.CraftAOE.AOE,
                    savingThrow: Kingmaker.Craft.CraftSavingThrow.Reflex,
                    spellType: Kingmaker.Craft.CraftSpellType.Damage)
                .SetIcon(spellicon)
                .Configure();
        }


        // edit
        internal static string feature = "HellBall-feature";
        internal static string featureguid = Guids.HellBallFeature;
        // don't edit
        internal static string featurename = $"{feature}.name";
        internal static string featuredescription = $"{feature}.description";

        public static void ConfigureFeatureDummy()
        {
            FeatureConfigurator.New(feature, featureguid).Configure();
        }
        public static void ConfigureFeatureEnabled(BlueprintAbility newspell)
        {
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddToGroups(Kingmaker.Blueprints.Classes.FeatureGroup.MythicFeat)
                .AddComponent(new SpellToBook()
                        {
                            spell = newspell,
                            spelllevel = 9,
                        })
                .Configure();
        }
    }
}

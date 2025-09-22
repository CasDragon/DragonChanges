using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.BasicEx;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.New_Components;
using DragonChanges.Utils;
using DragonLibrary.NewComponents;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;
using static TabletopTweaks.Core.MechanicsChanges.MetamagicExtention;

namespace DragonChanges.NewSpells
{
    internal class Hellball
    {
        // edit
        internal const string spell = "HellBall";
        internal const string spellguid = Guids.HellBall;
        internal const string settingName = "hellball";
        internal const string settingDescription = "Enable the HellBall spell, added from a Mythic Ability";
        internal const string settingiconName = "hellballicon";
        internal const string settingiconDescription = "Change the HellBall spell icon to the WRONG icon, which is worse, for Tomek (requires game restart)";
        // don't edit
        internal const string spellname = $"{spell}.name";
        internal const string spelldescription = $"{spell}.description";

        [DragonConfigure]
        [DragonSetting(SettingCategories.NewSpells, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
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
        [DragonSetting(SettingCategories.NewSpells, settingiconName, settingiconDescription)]
        public static BlueprintAbility ConfigureEnabled()
        {
            Metamagic metas = AbilityRefs.HellfireRay.Reference.Get().AvailableMetamagic;
            if (ModCompat.tttbase)
            {
                metas = metas | (Metamagic)(CustomMetamagic.Burning | CustomMetamagic.ElementalAcid |
                    CustomMetamagic.ElementalCold | CustomMetamagic.ElementalElectricity |
                    CustomMetamagic.ElementalFire | CustomMetamagic.Flaring | CustomMetamagic.Rime);
            }
            string spellicon = "Abilities.HellBall.png";
            if (SettingsAction.GetSetting<bool>("hellballicon"))
            {
                
                spellicon = "Abilities.HellBallWrong.png";
            }
            return AbilityConfigurator.NewSpell(spell, spellguid, SpellSchool.Evocation, true, SpellDescriptor.Fire | SpellDescriptor.Acid | SpellDescriptor.Sonic | SpellDescriptor.Electricity)
                .SetDisplayName(spellname)
                .SetDescription(spelldescription)
                // components
                .AddAbilityDeliverProjectile(
                        projectiles: [ProjectileRefs.Fireball00.Reference.Get()],
                        type: AbilityProjectileType.Simple,
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
                .SetIcon(MicroAssetUtil.GetAssemblyResourceSprite(spellicon))
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
                            spell = newspell.ToReference<BlueprintAbilityReference>(),
                            spelllevel = 9,
                        })
                .Configure();
        }
    }
}

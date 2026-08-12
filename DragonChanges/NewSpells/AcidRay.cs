using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using TabletopTweaks.Core.MechanicsChanges;

namespace DragonChanges.NewSpells;

public class AcidRay
{
        // edit
        internal const string spell = "AcidRay";
        internal const string spellguid = Guids.AcidRay;
        internal const string settingName = "elementalray";
        internal const string settingDescription = "Enable the elemental ray spells, copycats of HFR with different elements";
        // don't edit
        [DragonLocalizedString(spellname, "Hellacid Ray")]
        internal const string spellname = $"{spell}.name";
        [DragonLocalizedString(spelldescription, sdescription)]
        internal const string spelldescription = $"{spell}.description";

        internal const string sdescription =
            "A blast of hellacid blazes from your hands. You can fire one ray, plus one additional ray for every 4 {g|Encyclopedia:Caster_Level}caster levels{/g} " +
            "beyond 11th (to a maximum of three rays at 19th level). Each ray requires a ranged {g|Encyclopedia:TouchAttack}touch attack{/g} to hit and deals " +
            "{g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} per caster level (maximum 15d6). Half the damage is " +
            "{g|Encyclopedia:Energy_Damage}acid damage{/g}, but the other half results directly from unholy power and is therefore not subject " +
            "to being reduced by acid {g|Encyclopedia:Energy_Resistance}resistance{/g}.";
        
        [DragonConfigure]
        [DragonSetting(SettingCategories.NewSpells, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
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
                metas = metas | (Metamagic) (MetamagicExtention.CustomMetamagic.Burning | MetamagicExtention.CustomMetamagic.ElementalAcid |
                    MetamagicExtention.CustomMetamagic.ElementalCold | MetamagicExtention.CustomMetamagic.ElementalElectricity |
                    MetamagicExtention.CustomMetamagic.ElementalFire | MetamagicExtention.CustomMetamagic.Flaring);
            }
            string spritepath = "Abilities.HeavenFireRay.png";
            //if (SettingsAction.GetSetting<bool>("darthicons"))
            //    spritepath = "Darth.HeavenfireRay.png";
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
                        projectiles: [ProjectileRefs.RayOfEnfeeblement00.Reference.Get(),
                                ProjectileRefs.RayOfEnfeeblement00.Reference.Get(),
                                ProjectileRefs.RayOfEnfeeblement00.Reference.Get()],
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
                                        DamageTypes.Energy(DamageEnergyType.Acid),
                                        ContextDice.Value(DiceType.D6, ContextValues.Rank(AbilityRankType.Default)),
                                        half: true)
                                    .DealDamage(
                                        DamageTypes.Energy(DamageEnergyType.Unholy),
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
                .SetActionType(UnitCommand.CommandType.Standard)
                .SetAvailableMetamagic(metas)
                .AddCraftInfoComponent(
                    aOEType: Kingmaker.Craft.CraftAOE.None,
                    savingThrow: Kingmaker.Craft.CraftSavingThrow.None,
                    spellType: Kingmaker.Craft.CraftSpellType.Damage)
                .SetIcon(MicroAssetUtil.GetAssemblyResourceSprite(spritepath))
                .Configure();
        }
}
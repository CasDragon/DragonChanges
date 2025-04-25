using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Facts;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using DragonChanges.BPCoreExtensions;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Shields;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewEnchantments
{
    internal class StorvalsThunderEnchant
    {
        // edit
        internal static string enchantment = "StorvalsThunder-enchant";
        internal static string enchantmentguid = Guids.ThunderHammerEnchant;
        internal static string feature = "StorvalSetBonusFeature";
        internal static string featureguid = Guids.StorvalSetBonusFeature;
        internal static string buff = "StorvalSetBonusChecker";
        internal static string buffguid = Guids.StorvalSetBonusChecker;
        // don't edit
        internal static string enchantmentname = $"{enchantment}.name";
        internal static string enchantmentdescription = $"{enchantment}.description";
        public static BlueprintWeaponEnchantment ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid).Configure();
            return WeaponEnchantmentConfigurator.New(enchantment, enchantmentguid).Configure();
        }
        public static BlueprintWeaponEnchantment ConfigureEnabled(BlueprintItemShield shield)
        {
            BlueprintBuff actualbuff = BuffConfigurator.New(feature, featureguid)
                .AddIncreaseSpellDC(spell: Guids.ThunderHammerAbility, bonusDC: 5, spellsOnly: false)
                .AddBuffEnchantSpecificWeaponWorn(weaponBlueprint: Guids.ThunderHammer, enchantmentBlueprint: StorvalSetBonus.ConfigureEnabled())
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .Configure();
            BlueprintBuff feat = BuffConfigurator.New(buff, buffguid)
                .AddFactContextActions(activated: ActionsBuilder.New()
                    .Conditional(conditions: ConditionsBuilder.New()
                        .HasSpecificWeapon(shield, checkOnCaster: true, isShield: true),
                        ifTrue: ActionsBuilder.New().ApplyBuff(actualbuff, durationValue: ContextDuration.Fixed(1, DurationRate.Minutes))),
                    newRound: ActionsBuilder.New()
                    .Conditional(conditions: ConditionsBuilder.New()
                        .HasSpecificWeapon(shield, checkOnCaster: true, isShield: true),
                        ifTrue: ActionsBuilder.New().ApplyBuff(actualbuff, durationValue: ContextDuration.Fixed(2, DurationRate.Rounds))))
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .Configure();
            var thing = FeatureConfigurator.New("Storvalthingy", Guids.storavlthing)
                .AddFactContextActions(activated: ActionsBuilder.New().ApplyBuffPermanent(feat),
                    deactivated: ActionsBuilder.New().RemoveBuff(feat))
                .SetHideInCharacterSheetAndLevelUp(true)
                .SetHideInUI(true)
                .Configure();
            return WeaponEnchantmentConfigurator.New(enchantment, enchantmentguid)
                .SetEnchantName(enchantmentname)
                .SetDescription(enchantmentdescription)
                .AddWeaponEnergyDamageDice(DamageEnergyType.Electricity, new DiceFormula(rollsCount:1, diceType: DiceType.D6 ))
                .AddWeaponEnergyDamageDice(DamageEnergyType.Sonic, new DiceFormula(rollsCount: 1, diceType: DiceType.D6))
                .AddUnitFactEquipment(thing)
                .Configure();
        }
    }
}

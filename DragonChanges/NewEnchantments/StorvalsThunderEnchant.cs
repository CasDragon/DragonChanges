using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Buffs.Blueprints;
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
        internal static string featureguid = Guids.StorvalSetBonus;
        // don't edit
        internal static string enchantmentname = $"{enchantment}.name";
        internal static string enchantmentdescription = $"{enchantment}.description";
        public static BlueprintWeaponEnchantment ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid).Configure();
            return WeaponEnchantmentConfigurator.New(enchantment, enchantmentguid).Configure();
        }
        public static BlueprintWeaponEnchantment ConfigureEnabled()
        {
            BlueprintBuff feat = BuffConfigurator.New(feature, featureguid)
                .AddBuffActions(activated: ActionsBuilder.New()
                    .Conditional(conditions: ConditionsBuilder.New()
                        .HasBuff()
                    )
                .AddIncreaseSpellDC(spell: Guids.ThunderHammerAbility, value: ContextValues.Constant(5), descriptor: ModifierDescriptor.UntypedStackable)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .Configure();
            return WeaponEnchantmentConfigurator.New(enchantment, enchantmentguid)
                .SetEnchantName(enchantmentname)
                .SetDescription(enchantmentdescription)
                .AddWeaponEnergyDamageDice(DamageEnergyType.Electricity, new DiceFormula() { m_Rolls = 1, m_Dice = DiceType.D6 })
                .AddWeaponEnergyDamageDice(DamageEnergyType.Sonic, new DiceFormula() { m_Rolls = 1, m_Dice = DiceType.D6 })
                .AddUnitFeatureEquipment(feat)
                .Configure();
        }
    }
}

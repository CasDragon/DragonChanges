using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
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
        // don't edit
        internal static string enchantmentname = $"{enchantment}.name";
        internal static string enchantmentdescription = $"{enchantment}.description";
        public static BlueprintWeaponEnchantment ConfigureDummy()
        {
            return WeaponEnchantmentConfigurator.New(enchantment, enchantmentguid).Configure();
        }
        public static BlueprintWeaponEnchantment ConfigureEnabled()
        {
            return WeaponEnchantmentConfigurator.New(enchantment, enchantmentguid)
                .SetEnchantName(enchantmentname)
                .SetDescription(enchantmentdescription)
                .AddWeaponEnergyDamageDice(DamageEnergyType.Electricity, new DiceFormula() { m_Rolls = 1, m_Dice = DiceType.D6 })
                .AddWeaponEnergyDamageDice(DamageEnergyType.Sonic, new DiceFormula() { m_Rolls = 1, m_Dice = DiceType.D6 })
                .Configure();
        }
    }
}

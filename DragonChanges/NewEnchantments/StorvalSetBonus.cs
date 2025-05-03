using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;

namespace DragonChanges.NewEnchantments
{
    internal class StorvalSetBonus
    {
        // edit
        internal const string enchantment = "StorvalSetBonus";
        internal const string enchantmentguid = Guids.StorvalSetBonus;
        // don't edit
        internal const string enchantmentname = $"{enchantment}.name";
        internal const string enchantmentdescription = $"{enchantment}.description";
        public static void ConfigureDummy()
        {
            WeaponEnchantmentConfigurator.New(enchantment, enchantmentguid).Configure();
        }
        public static BlueprintWeaponEnchantment ConfigureEnabled()
        {
            return WeaponEnchantmentConfigurator.New(enchantment, enchantmentguid)
                .SetEnchantName(enchantmentname)
                .SetDescription(enchantmentdescription)
                .AddWeaponEnergyDamageDice(DamageEnergyType.Electricity, new DiceFormula(rollsCount: 2, diceType: DiceType.D6))
                .Configure();
        }
    }
}

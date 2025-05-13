using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.templates
{
    internal class EnchantmentTemplate
    {
        // edit
        internal const string enchantment = "enchantment";
        internal const string enchantmentguid = Guids.Template;
        internal const string settingName = "";
        internal const string settingDescription = "";
        // don't edit
        internal const string enchantmentname = $"{enchantment}.name";
        internal const string enchantmentdescription = $"{enchantment}.description";
        //[DragonConfigure]
        //[DragonSetting(settingCategories.NewAbilities, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{enchantment} enchantment enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{enchantment} disabled, configuring dummy");
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            WeaponEnchantmentConfigurator.New(enchantment, enchantmentguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            WeaponEnchantmentConfigurator.New(enchantment, enchantmentguid)
                .SetEnchantName(enchantmentname)
                .SetDescription(enchantmentdescription)
                .Configure();
        }
    }
}

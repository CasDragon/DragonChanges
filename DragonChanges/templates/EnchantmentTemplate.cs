using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.Utils;

namespace DragonChanges.templates
{
    internal class EnchantmentTemplate
    {
        // edit
        internal const string enchantment = "";
        internal const string enchantmentguid = Guids.Template;
        internal const string settingName = "";
        internal const string settingDescription = "";
        internal const string enchantmentname = "";
        internal const string enchantmentdescription = "";
        // don't edit
        //[DragonLocalizedString(enchantmentnamekey, enchantmentname)]
        internal const string enchantmentnamekey = $"{enchantment}.name";
        //[DragonLocalizedString(enchantmentdescriptionkey, enchantmentdescription)]
        internal const string enchantmentdescriptionkey = $"{enchantment}.description";
        //[DragonConfigure]
        //[DragonSetting(SettingCategories.NewAbilities, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
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

﻿using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
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
        internal static string enchantment = "enchantment";
        internal static string enchantmentguid = Guids.ThunderHammer;
        // don't edit
        internal static string enchantmentname = $"{enchantment}.name";
        internal static string enchantmentdescription = $"{enchantment}.description";
        //[DragonConfigure]
        public static void Configure()
        {
            if (Settings.GetSetting<bool>(enchantment.ToLower()))
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

using BlueprintCore.Blueprints.Configurators.Items;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.Content
{
    internal class Thwarter
    {
        public const string item = "Thwarter";
        [DragonConfigure]
        public static void Configure()
        {
            if (Settings.GetSetting<bool>(item.ToLower()))
            {
                Main.log.Log($"{item} item enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{item} disabled, skipping");
                //ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            //ItemConfigurator.New(item, itemguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            ItemWeaponConfigurator.For(ItemWeaponRefs.JustifierTongiItem)
                .SetVisualParameters(ItemWeaponRefs.TongiKeenPlus1.Reference.Get().VisualParameters)
                .Configure();
        }
    }
}

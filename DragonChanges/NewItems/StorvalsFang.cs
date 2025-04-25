using BlueprintCore.Blueprints.Configurators.Items;
using BlueprintCore.Blueprints.Configurators.Items.Shields;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewItems
{
    internal class StorvalsFang
    {
        // edit
        internal static string item = "StorvalsFang";
        internal static string itemguid = Guids.ThunderShield;
        internal static string buff = "StorvalsFang-buff";
        internal static string buffguid = Guids.ThunderShieldBuff;
        // don't edit
        internal static string itemname = $"{item}.name";
        internal static string itemdescription = $"{item}.description";
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
                Main.log.Log($"{item} disabled, configuring dummy");
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            BuffConfigurator.New(buff, buffguid).Configure();
            ItemShieldConfigurator.New(item, itemguid).Configure();
            SpikedLightShieldPlus1Sonic.ConfigureDummy();
        }
        public static void ConfigureEnabled()
        {
            ItemShieldConfigurator.New(item, itemguid)
                .SetDisplayNameText(itemname)
                .SetDescriptionText(itemdescription)
                .SetCost(5000)
                .SetDestructible(true)
                .SetShardItem(ItemRefs.MetalShardItem.Reference.Get())
                .SetCR(5)
                .SetVisualParameters(ItemShieldRefs.FleshTearerShieldItem.Reference.Get().VisualParameters)
                .SetWeaponComponent(SpikedLightShieldPlus1Sonic.ConfigureEnabled())
                .SetArmorComponent(ItemArmorRefs.LightShieldArmorItemPlus1.Reference.Get())
                .Configure();
        }
    }
}

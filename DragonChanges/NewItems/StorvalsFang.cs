using BlueprintCore.Blueprints.Configurators.Items;
using BlueprintCore.Blueprints.Configurators.Items.Shields;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Items.Shields;
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
        internal const string item = "StorvalsFang";
        internal const string itemguid = Guids.ThunderShield;
        // don't edit
        internal const string itemname = item + ".name";
        internal const string itemdescription = item + ".description";
        public static void ConfigureDummy()
        {
            ItemShieldConfigurator.New(item, itemguid).Configure();
            SpikedLightShieldPlus1Sonic.ConfigureDummy();
        }
        public static BlueprintItemShield ConfigureEnabled()
        {
            BlueprintItemShield shield = ItemShieldConfigurator.New(item, itemguid)
                .SetDisplayNameText(itemname)
                .SetDescriptionText(itemdescription)
                .SetCost(5000)
                .SetDestructible(true)
                .SetShardItem(ItemRefs.MetalShardItem.Reference.Get())
                .SetCR(5)
                .SetVisualParameters(ItemShieldRefs.FleshTearerShieldItem.Reference.Get().VisualParameters)
                .SetWeaponComponent(SpikedLightShieldPlus1Sonic.ConfigureEnabled())
                .SetArmorComponent(StorvalsFangArmor.ConfigureEnabled())
                .Configure();
            AneviaVendor.AddItem(shield);
            return shield;
        }
    }
}

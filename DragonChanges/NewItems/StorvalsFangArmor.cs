using BlueprintCore.Blueprints.Configurators.Items.Armors;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewItems
{
    internal class StorvalsFangArmor
    {
        // edit
        internal const string item = "LightShieldArmorItemPlus1LightningResist";
        internal const string itemguid = Guids.LightShieldArmorItemPlus1LightningResist;
        // don't edit
        internal const string itemname = item + ".name";
        internal const string itemdescription = item + ".description";
        public static void ConfigureDummy()
        {
            ItemArmorConfigurator.New(item, itemguid).Configure();
        }
        public static BlueprintItemArmor ConfigureEnabled()
        {
            return ItemArmorConfigurator.New(item, itemguid)
                .CopyFrom(ItemArmorRefs.LightShieldArmorItem)
                .SetDisplayNameText(itemname)
                .SetDescriptionText(itemdescription)
                .SetEnchantments([ArmorEnchantmentRefs.ShieldEnhancementBonus1.Reference.Get(),
                                  ArmorEnchantmentRefs.ElectricityResistance10Enchant.Reference.Get()])
                .Configure();
        }
    }
}

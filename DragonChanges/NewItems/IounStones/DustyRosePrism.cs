using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewItems.IounStones.Abilities;
using DragonChanges.NewItems.IounStones.Buffs;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Items.Equipment;

namespace DragonChanges.NewItems.IounStones
{
    internal class DustyRosePrism
    {
        // edit
        internal const string item = "DustyRosePrismItem";
        internal const string itemguid = Guids.DustyRosePrismItem;
        internal const string settingName = "iounstones";
        // don't edit
        [DragonLocalizedString(itemname, "Dusty Rose Prism - Ioun Stone")]
        internal const string itemname = $"{item}.name";
        [DragonLocalizedString(itemdescription, "This stone grants the wearer a +5 insight bonus to AC.")]
        internal const string itemdescription = $"{item}.description";
        [DragonConfigure]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{item} item enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{item} disabled, configuring dummy");
                ConfigureDummy();
                DustyRosePrismAbility.ConfigureDummy();
                DustyRosePrismBuff.ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            ItemEquipmentUsableConfigurator.New(item, itemguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            BlueprintItemEquipmentUsable itembp = ItemEquipmentUsableConfigurator.New(item, itemguid)
                .SetDisplayNameText(itemname)
                .SetDescriptionText(itemdescription)
                .SetIcon(ItemRefs.Bloodstone.Reference.Get().Icon)
                .SetCost(50000)
                .SetWeight(0)
                .SetInventoryPutSound("CommonPut")
                .SetInventoryTakeSound("CommonTake")
                .SetInventoryEquipSound("CommonPut")
                .SetHideAbilityInfo(true)
                .SetType(UsableItemType.Other)
                .SetActivatableAbility(DustyRosePrismAbility.ConfigureEnabled())
                .Configure();
            AneviaVendor.AddItem(itembp, 20);
        }
    }
}

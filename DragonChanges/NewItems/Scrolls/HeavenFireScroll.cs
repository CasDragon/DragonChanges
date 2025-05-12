using BlueprintCore.Blueprints.Configurators.Items;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewItems.Scrolls
{
    internal class HeavenFireScroll
    {
        // edit
        internal const string item = "HeavenFireRayscroll";
        internal const string itemguid = Guids.HeavenFireScroll;
        [DragonConfigure(1)]
        public static void Configure()
        {
            if (Settings.GetSetting<bool>("heavenray"))
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
            ItemEquipmentUsableConfigurator.New(item, itemguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            BlueprintItemEquipmentUsable scroll = ItemEquipmentUsableConfigurator.New(item, itemguid)
                .SetCost(1650)
                .SetWeight(0.2f)
                .SetDestructible(true)
                .SetShardItem(ItemRefs.PaperShardItem.Reference.Get())
                .SetInventoryPutSound("ScrollPut")
                .SetInventoryTakeSound("ScrollTake")
                .SetTrashLootTypes(TrashLootType.Scrolls | TrashLootType.Scrolls_RE)
                .SetAbility(Guids.HeavenRay)
                .SetSpendCharges(true)
                .SetCharges(1)
                .SetCasterLevel(11)
                .SetSpellLevel(6)
                .SetType(UsableItemType.Scroll)
                .AddCopyScroll()
                .SetIcon(ItemEquipmentUsableRefs.ScrollOfHellfireRay.Reference.Get().Icon)
                .Configure();
            AneviaVendor.AddItem(scroll);
        }
    }
}

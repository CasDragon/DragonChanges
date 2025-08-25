using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;

namespace DragonChanges.NewItems.Scrolls
{
    internal class CommunalDeathWardScroll
    {
        // edit
        internal const string item = "CommunalDeathWardScroll";
        internal const string itemguid = Guids.CommunalDeathWardScroll;
        internal const string settingName = "communaldeathward";
        public static void ConfigureDummy()
        {
            ItemEquipmentUsableConfigurator.New(item, itemguid).Configure();
        }
        public static void ConfigureEnabled(BlueprintAbility ability)
        {
            BlueprintItemEquipmentUsable scroll = ItemEquipmentUsableConfigurator.New(item, itemguid)
                .SetCost(2650)
                .SetWeight(0.2f)
                .SetDestructible(true)
                .SetShardItem(ItemRefs.PaperShardItem.Reference.Get())
                .SetInventoryPutSound("ScrollPut")
                .SetInventoryTakeSound("ScrollTake")
                .SetTrashLootTypes(TrashLootType.Scrolls | TrashLootType.Scrolls_RE)
                .SetAbility(ability)
                .SetSpendCharges(true)
                .SetCharges(1)
                .SetCasterLevel(11)
                .SetSpellLevel(7)
                .SetType(UsableItemType.Scroll)
                .AddCopyScroll()
                .SetIcon(ItemEquipmentUsableRefs.ScrollOfDeathWard.Reference.Get().Icon)
                .Configure();
            AneviaVendor.AddItem(scroll, 99);
        }
    }
}

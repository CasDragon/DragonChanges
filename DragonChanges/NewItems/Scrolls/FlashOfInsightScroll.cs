using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using UnityEngine;

namespace DragonChanges.NewItems.Scrolls;

public class FlashOfInsightScroll
{
    // edit
    internal const string item = "flashofinsightscroll";
    internal const string itemguid = Guids.FlashOfInsightScroll;
    
    public static void ConfigureDummy()
    {
        ItemEquipmentUsableConfigurator.New(item, itemguid).Configure();
    }
    public static void ConfigureEnabled(BlueprintAbility ability, Sprite icon)
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
            .SetSpellLevel(2)
            .SetType(UsableItemType.Scroll)
            .AddCopyScroll()
            .SetIcon(icon)
            .Configure();
        AneviaVendor.AddItem(scroll, 99);
    }
}
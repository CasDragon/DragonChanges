using BlueprintCore.Blueprints.Configurators.Items;
using BlueprintCore.Blueprints.Configurators.Items.Armors;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace DragonChanges.NewItems
{
    internal class MithralFullBarding
    {
        // edit
        internal const string item = "mithfullbarding";
        internal const string itemguid = Guids.mithfullbarding;
        internal const string settingName = "mirthralbarding";
        internal const string settingDescription = "Adds new Barding armor that is Mithral";
        internal const string itemname = "Mithral Full Barding";
        internal const string itemplus1 = "mithfullbardingplus1";
        internal const string itemguidplus1 = Guids.mithfullbarding1;
        internal const string itemnameplus1 = "Mithral Full Barding +1";
        internal const string itemplus2 = "mithfullbardingplus2";
        internal const string itemguidplus2 = Guids.mithfullbarding2;
        internal const string itemnameplus2 = "Mithral Full Barding +2";
        internal const string itemplus3 = "mithfullbardingplus3";
        internal const string itemguidplus3 = Guids.mithfullbarding3;
        internal const string itemnameplus3 = "Mithral Full Barding +3";
        internal const string itemplus4 = "mithfullbardingplus4";
        internal const string itemguidplus4 = Guids.mithfullbarding4;
        internal const string itemnameplus4 = "Mithral Full Barding +4";
        internal const string itemplus5 = "mithfullbardingplus5";
        internal const string itemguidplus5 = Guids.mithfullbarding5;
        internal const string itemnameplus5 = "Mithral Full Barding +5";
        // don't edit
        [DragonLocalizedString(itemnamekey, itemname)]
        internal const string itemnamekey = $"{item}.name";
        [DragonLocalizedString(itemnamekeyplus1, itemnameplus1)]
        internal const string itemnamekeyplus1 = $"{itemplus1}.name";
        [DragonLocalizedString(itemnamekeyplus2, itemnameplus2)]
        internal const string itemnamekeyplus2 = $"{itemplus2}.name";
        [DragonLocalizedString(itemnamekeyplus3, itemnameplus3)]
        internal const string itemnamekeyplus3 = $"{itemplus3}.name";
        [DragonLocalizedString(itemnamekeyplus4, itemnameplus4)]
        internal const string itemnamekeyplus4 = $"{itemplus4}.name";
        [DragonLocalizedString(itemnamekeyplus5, itemnameplus5)]
        internal const string itemnamekeyplus5 = $"{itemplus5}.name";
        [DragonConfigure]
        [DragonSetting(SettingCategories.NewItems, settingName, settingDescription)]
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
            }
        }
        public static void ConfigureDummy()
        {
            ItemArmorConfigurator.New(item, itemguid)
                .SetDisplayNameText(itemnamekey)
                .SetDescriptionText(LocalizedStringHelper.disabledcontentstring)
                .Configure();
        }
        public static void ConfigureEnabled()
        {
            var itemref = ItemArmorRefs.FullBardingStandartPlus1.Reference.Get();
            var mithralenchant = ArmorEnchantmentRefs.MithralArmorEnchant.Reference.Get();
            var item1 = ItemArmorConfigurator.New(item, itemguid)
                .SetDisplayNameText(itemnamekey)
                .AddEquipmentRestrictionHasAnyClassFromList([CharacterClassRefs.AnimalCompanionClass.Reference.Get(),
                    CharacterClassRefs.DragonClass.Reference.Get()])
                .SetIcon(itemref.Icon)
                .SetCost(2000)
                .SetWeight(0.0f)
                .SetTrashLootTypes(Kingmaker.Enums.TrashLootType.Equipment_RE | Kingmaker.Enums.TrashLootType.Equipment_Trickster)
                .SetCR(5)
                .SetEquipmentEntity(itemref.m_EquipmentEntity)
                .SetForcedRampColorPresetIndex(2)
                .SetType(itemref.m_Type)
                .SetEnchantments([mithralenchant])
                .Configure();
            var item2 = ItemArmorConfigurator.New(itemplus1, itemguidplus1)
                .SetDisplayNameText(itemnamekeyplus1)
                .AddEquipmentRestrictionHasAnyClassFromList([CharacterClassRefs.AnimalCompanionClass.Reference.Get(),
                    CharacterClassRefs.DragonClass.Reference.Get()])
                .SetIcon(itemref.Icon)
                .SetCost(3500)
                .SetWeight(0.0f)
                .SetTrashLootTypes(Kingmaker.Enums.TrashLootType.Equipment_RE | Kingmaker.Enums.TrashLootType.Equipment_Trickster)
                .SetCR(5)
                .SetEquipmentEntity(itemref.m_EquipmentEntity)
                .SetForcedRampColorPresetIndex(2)
                .SetType(itemref.m_Type)
                .SetEnchantments([mithralenchant,
                    ArmorEnchantmentRefs.ArmorEnhancementBonus1.Reference.Get()])
                .Configure();
            var item3 = ItemArmorConfigurator.New(itemplus2, itemguidplus2)
                .SetDisplayNameText(itemnamekeyplus2)
                .AddEquipmentRestrictionHasAnyClassFromList([CharacterClassRefs.AnimalCompanionClass.Reference.Get(),
                    CharacterClassRefs.DragonClass.Reference.Get()])
                .SetIcon(itemref.Icon)
                .SetCost(6000)
                .SetWeight(0.0f)
                .SetTrashLootTypes(Kingmaker.Enums.TrashLootType.Equipment_RE | Kingmaker.Enums.TrashLootType.Equipment_Trickster)
                .SetCR(5)
                .SetEquipmentEntity(itemref.m_EquipmentEntity)
                .SetForcedRampColorPresetIndex(2)
                .SetType(itemref.m_Type)
                .SetEnchantments([mithralenchant,
                    ArmorEnchantmentRefs.ArmorEnhancementBonus2.Reference.Get()])
                .Configure();
            var item4 = ItemArmorConfigurator.New(itemplus3, itemguidplus3)
                .SetDisplayNameText(itemnamekeyplus3)
                .AddEquipmentRestrictionHasAnyClassFromList([CharacterClassRefs.AnimalCompanionClass.Reference.Get(),
                    CharacterClassRefs.DragonClass.Reference.Get()])
                .SetIcon(itemref.Icon)
                .SetCost(12000)
                .SetWeight(0.0f)
                .SetTrashLootTypes(Kingmaker.Enums.TrashLootType.Equipment_RE | Kingmaker.Enums.TrashLootType.Equipment_Trickster)
                .SetCR(5)
                .SetEquipmentEntity(itemref.m_EquipmentEntity)
                .SetForcedRampColorPresetIndex(2)
                .SetType(itemref.m_Type)
                .SetEnchantments([mithralenchant,
                    ArmorEnchantmentRefs.ArmorEnhancementBonus3.Reference.Get()])
                .Configure();
            var item5 = ItemArmorConfigurator.New(itemplus4, itemguidplus4)
                .SetDisplayNameText(itemnamekeyplus4)
                .AddEquipmentRestrictionHasAnyClassFromList([CharacterClassRefs.AnimalCompanionClass.Reference.Get(),
                    CharacterClassRefs.DragonClass.Reference.Get()])
                .SetIcon(itemref.Icon)
                .SetCost(22000)
                .SetWeight(0.0f)
                .SetTrashLootTypes(Kingmaker.Enums.TrashLootType.Equipment_RE | Kingmaker.Enums.TrashLootType.Equipment_Trickster)
                .SetCR(5)
                .SetEquipmentEntity(itemref.m_EquipmentEntity)
                .SetForcedRampColorPresetIndex(2)
                .SetType(itemref.m_Type)
                .SetEnchantments([mithralenchant,
                    ArmorEnchantmentRefs.ArmorEnhancementBonus4.Reference.Get()])
                .Configure();
            var item6 = ItemArmorConfigurator.New(itemplus5, itemguidplus5)
                .SetDisplayNameText(itemnamekeyplus5)
                .AddEquipmentRestrictionHasAnyClassFromList([CharacterClassRefs.AnimalCompanionClass.Reference.Get(),
                    CharacterClassRefs.DragonClass.Reference.Get()])
                .SetIcon(itemref.Icon)
                .SetCost(30000)
                .SetWeight(0.0f)
                .SetTrashLootTypes(Kingmaker.Enums.TrashLootType.Equipment_RE | Kingmaker.Enums.TrashLootType.Equipment_Trickster)
                .SetCR(5)
                .SetEquipmentEntity(itemref.m_EquipmentEntity)
                .SetForcedRampColorPresetIndex(2)
                .SetType(itemref.m_Type)
                .SetEnchantments([mithralenchant,
                    ArmorEnchantmentRefs.ArmorEnhancementBonus5.Reference.Get()])
                .Configure();
            AneviaVendor.AddItem(item1, 10);
            AneviaVendor.AddItem(item2, 10);
            AneviaVendor.AddItem(item3, 10);
            AneviaVendor.AddItem(item4, 10);
            AneviaVendor.AddItem(item5, 10);
            AneviaVendor.AddItem(item6, 10);
        }
    }
}

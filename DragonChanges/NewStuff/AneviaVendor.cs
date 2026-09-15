using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.MiscEx;
using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.Configurators.DialogSystem;
using BlueprintCore.Blueprints.Configurators.Items;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewStuff.VendorStuff;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Loot;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;
using Kingmaker.DialogSystem.Blueprints;
using Kingmaker.EntitySystem.Persistence.Versioning;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonChanges.NewStuff
{
    internal class AneviaVendor
    {
        // edit
        internal const string vending = "AneviaVendor";
        internal const string vendingguid = Guids.AneviaVendorLootTable;
        internal const string answer = "AneviaVendorAnswer";
        internal const string answerguid = Guids.AneviaVendorAnswer;
        // don't edit
        internal const string answertext = $"{answer}.text";
        internal static SharedVendorTableConfigurator aneviatable = null;
        internal static readonly BlueprintCore.Utils.Blueprint<BlueprintReference<BlueprintUnit>>[] aneviaunits =
                [UnitRefs.AneviaTirabade, UnitRefs.AneviaTirabade_DH, UnitRefs.AneviaTirabade_DrezenCapital
                ];

        private static readonly string[] answerlists = ["3e6231392987747479e12f77e8f44611", "33960c7f7af40cd43b7f801a76c87a0b"];

        public static void ConfigureStart()
        {
            Main.log.Log("Starting to create vendor list");
            aneviatable = SharedVendorTableConfigurator.New(vending, vendingguid);
        }
        
        internal const string settingName = "vendor";
        internal const string settingDescription = "Adds a new vendor to every act, a Wooloo. ";
        [DragonSetting(SettingCategories.None, settingName, settingDescription)]
        public static void DoDLCSpawner(BlueprintSharedVendorTable loottable)
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log("Wooloo Vendor enabled");
                var dlcvendor = VendorUnit.CreateVendorBlueprint(loottable);
                var vendor = new VendorSpawner();
                EventBus.Subscribe(vendor);
            }
            else
            {
                Main.log.Log("Wooloo Vendor disabled");
            }
        }
        internal const string asettingName = "vendor-anevia";
        internal const string asettingDescription = "Also adds the vendor table to Anevia, for those that hate sheep ";
        [DragonSetting(SettingCategories.None, asettingName, asettingDescription)]
        public static void ConfigureEnd()
        {
            Main.log.Log("Attempting to finish vendor list");
            AddItemsSorted();
            BlueprintSharedVendorTable loottable = aneviatable.Configure();
            BlueprintUnitUpgrader vendorupgrader = VendorUnitUpgrader.Configure(loottable);
            DoDLCSpawner(loottable);
            BlueprintAnswer newanswer = AnswerConfigurator.New(answer, answerguid)
                .SetText(answertext)
                .SetOnSelect(ActionsBuilder.New().StartTrade(new DialogFirstSpeaker()))
                .Configure();
            foreach (var unit in aneviaunits)
            {
                UnitConfigurator.For(unit)
                    .AddSharedVendor(loottable)
                    .Configure();
            }
            if (SettingsAction.GetSetting<bool>(asettingName))
            {
                Main.log.Log("Adding vendor to Anevia because people hate Wooloo");
                foreach (string alist in answerlists)
                {
                    AnswersListConfigurator.For(alist)
                        .AddToAnswers(newanswer)
                        .Configure();
                }
            }

            Main.log.Log("Anevia vendor created!");
        }

        public enum ItemType
        {
            None,
            Weapon,
            Armor,
            Helmet,
            Shield,
            Ring,
            Necklace,
            Scroll,
            Useable,
            Potion,
            IounStone,
            PetArmor,
        }

        public static void AddItem(BlueprintItem? item, int amount = 1)
        {
            AddItem(item, ItemType.None, amount);
        }

        public static void AddItem(BlueprintItem? item, ItemType itemType, int amount = 1)
        {
            if (item == null || amount < 1)
                return;
            itemDict.Add((item,  amount, itemType));
        }
        
        public static List<(BlueprintItem? item, int amount, ItemType itemType)> itemDict = [];
        public static void AddItemsSorted()
        {
            var items = itemDict.OrderBy(x => x.itemType)
                .ThenBy(x => x.item.NameSafe()).ToList();
            foreach (var itemtuple in items)
            {
                aneviatable.AddLootItemsPackFixed(itemtuple.amount, new LootItem() 
                    { m_Item = itemtuple.item.ToReference<BlueprintItemReference>(), m_Type = LootItemType.Item });
            }
        }
    }
}

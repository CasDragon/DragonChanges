using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.MiscEx;
using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.Configurators.DialogSystem;
using BlueprintCore.Blueprints.Configurators.Items;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Loot;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;
using Kingmaker.DialogSystem.Blueprints;
using Kingmaker.EntitySystem.Persistence.Versioning;

namespace DragonChanges.NewStuff
{
    internal class AneviaVendor
    {
        // edit
        internal static string vending = "AneviaVendor";
        internal static string vendingguid = Guids.AneviaVendorLootTable;
        internal static string answer = "AneviaVendorAnswer";
        internal static string answerguid = Guids.AneviaVendorAnswer;
        // don't edit
        internal static string answertext = $"{answer}.text";
        internal static SharedVendorTableConfigurator aneviatable = null;
        internal static BlueprintCore.Utils.Blueprint<BlueprintReference<BlueprintUnit>>[] aneviaunits = 
                [UnitRefs.AneviaTirabade, UnitRefs.AneviaTirabade_DH, UnitRefs.AneviaTirabade_DrezenCapital,
                UnitRefs.AneviaTirabade_GorgoyleAttack, UnitRefs.AneviaTirabade_LostChapel];
        internal static string[] answerlists = ["3e6231392987747479e12f77e8f44611", "33960c7f7af40cd43b7f801a76c87a0b"];

        public static void ConfigureStart()
        {
            Main.log.Log("Starting to create Anevia vendor");
            aneviatable = SharedVendorTableConfigurator.New(vending, vendingguid);
                
        }
        public static void ConfigureEnd()
        {
            Main.log.Log("Attempting to finsh Anevia vendor");
            BlueprintSharedVendorTable loottable = aneviatable.Configure();
            BlueprintUnitUpgrader vendorupgrader = VendorUnitUpgrader.Configure(loottable);
            foreach (var unit in aneviaunits)
            {
                UnitConfigurator.For(unit.Reference.Get())
                    .AddSharedVendor(loottable)
                    .AddUnitUpgraderComponent(upgraders: [vendorupgrader])
                    .Configure();
            }
            BlueprintAnswer newanswer = AnswerConfigurator.New(answer, answerguid)
                .SetText(answertext)
                .SetOnSelect(ActionsBuilder.New().StartTrade(new DialogFirstSpeaker()))
                .Configure();
            foreach (string alist in answerlists)
            {
                AnswersListConfigurator.For(alist)
                    .AddToAnswers(newanswer)
                    .Configure();
            }
            Main.log.Log("Anevia vendor created!");
        }
        public static void AddItem(BlueprintItem item, int amount = 1)
        {
            if (item == null || amount < 1)
                return;
            aneviatable.AddLootItemsPackFixed(amount, new LootItem() { m_Item = item.ToReference<BlueprintItemReference>(), m_Type = LootItemType.Item });
        }
    }
}

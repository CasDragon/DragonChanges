using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.Configurators.EntitySystem;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Items;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;
using Kingmaker.EntitySystem.Persistence.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewStuff
{
    internal class VendorUnitUpgrader
    {
        // edit
        internal static string upgrader = "VendorUpgrader";
        internal static string upgraderguid = Guids.VendorUpgrader;
        public static BlueprintUnitUpgrader Configure(BlueprintSharedVendorTable vendorTable)
        {
            BlueprintUnitUpgrader x = UnitUpgraderConfigurator.New(upgrader, upgraderguid)
                //.SetActions(ActionsBuilder.New().Add(new New_Components.UpgradeUnitsToVendors()))
                .Configure();
            x.Actions.Actions.Append(new New_Components.UpgradeUnitsToVendors() {vendortable=vendorTable});
            return x;
        }
        public static void ConfigureDummy()
        {
            UnitUpgraderConfigurator.New(upgrader, upgraderguid).Configure();
        }
    }
}

using Kingmaker.Blueprints.Items;
using Kingmaker;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Persistence.Versioning;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using UnityEngine;

namespace DragonChanges.New_Components
{
    [TypeId("4F5D96D8-C7B6-42B1-B7AD-935685242164")]
    [Serializable]
    internal class UpgradeUnitsToVendors : UnitUpgraderOnlyAction
    {
        [SerializeField]
        public BlueprintSharedVendorTable vendortable;
        public override string GetCaption()
        {
            return "Makes sure the non-vendor unit gets the vendor table";
        }

        public override void RunActionOverride()
        {
            UnitEntityData target = base.Target;
            if (target == null)
            {
                return;
            }
            target.Ensure<UnitPartVendor>().SetSharedInventory(vendortable);
            //UnitPartVendor unitPartVendor = target.Get<UnitPartVendor>() ?? new UnitPartVendor();
            //unitPartVendor.SetSharedInventory(vendortable);
        }
    }
}

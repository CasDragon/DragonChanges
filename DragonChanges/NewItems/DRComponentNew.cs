using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonChanges.NewItems
{
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    [AllowedOn(typeof(BlueprintUnit), false)]
    [AllowMultipleComponents]
    [TypeId("7F9C12C0-F358-480F-AB89-F66B7D4E7E80")]
    [Serializable]
    internal class DRComponentNew: AddDamageResistanceHardness
    {
        public bool stackable = true;

        public override bool IsStackable => base.IsStackable;
    }
}

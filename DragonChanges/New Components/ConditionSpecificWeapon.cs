using Kingmaker.EntitySystem.Entities;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Utility;
using Kingmaker;
using System;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items.Slots;
using Kingmaker.Blueprints.Items.Weapons;

namespace DragonChanges.New_Components
{
    [TypeId("54600673-2CCD-4A65-BEBE-768A652B8CB7")]
    internal class ConditionSpecificWeapon : ContextConditionIsWeaponEquipped
    {
        public BlueprintItemWeapon weapon;
        public override bool CheckCondition()
        {
            return base.CheckCondition() && CheckWeapons();
        }
        public bool CheckWeapons()
        {
            UnitEntityData unitEntityData = (this.CheckOnCaster ? base.Context.MaybeCaster : base.Target.Unit);
            HandSlot slot1 = unitEntityData.Body.SecondaryHand;
            bool flag1 = false;
            if (!slot1.HasWeapon || slot1.MaybeItem == null)
            {
                flag1 = false;
            }
            else
            {
                flag1 = slot1.Weapon.Blueprint.AssetGuid.Equals(weapon.AssetGuid);
            }
            HandSlot slot2 = unitEntityData.Body.SecondaryHand;
            bool flag2 = false;
            if (!slot2.HasWeapon || slot2.MaybeItem == null)
            {
                 flag2 = false;
            }
            else
            {
                flag2 = slot2.Weapon.Blueprint.AssetGuid.Equals(weapon.AssetGuid);
            }
            return flag1 || flag2;
        }
    }
}

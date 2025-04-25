using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Mechanics.Conditions;
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
            HandSlot slot1 = unitEntityData.Body.PrimaryHand;
            HandSlot slot2 = unitEntityData.Body.SecondaryHand;
            bool flag1;
            bool flag2;
            if (slot1.MaybeItem == null)
            {
                flag1 = false;
            }
            else
            {
                flag1 = slot1.Weapon.Blueprint.Equals(weapon);
            }
            if (slot2.MaybeItem == null)
            {
                 flag2 = false;
            }
            else
            {
                flag2 = slot2.Weapon.Blueprint.Equals(weapon);
            }
            return flag1 || flag2;
        }
    }
}

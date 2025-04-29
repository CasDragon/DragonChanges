using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items.Slots;
using Kingmaker.Blueprints.Items.Weapons;
using BlueprintCore.Conditions.Builder.ContextEx;
using Kingmaker.Blueprints;
using System;
using UnityEngine;

namespace DragonChanges.New_Components
{
    [TypeId("54600673-2CCD-4A65-BEBE-768A652B8CB7")]
    [Serializable]
    internal class ConditionSpecificWeapon : ContextConditionIsWeaponEquipped
    {
        [SerializeField]
        public SimpleBlueprint weapon; 
        [SerializeField]
        public bool isShield = false;
        public override bool CheckCondition()
        {
            return base.CheckCondition() && CheckWeapons();
        }
        public bool CheckWeapons()
        {
            UnitEntityData unitEntityData = (this.CheckOnCaster ? base.Context.MaybeCaster : base.Target.Unit);
            var body = unitEntityData.Body;
            if ( isShield)
                return body.PrimaryHand.MaybeShield?.Blueprint == weapon || body.SecondaryHand.MaybeShield?.Blueprint == weapon;
            else
                return body.PrimaryHand.MaybeWeapon?.Blueprint == weapon || body.SecondaryHand.MaybeWeapon?.Blueprint == weapon;
        }
    }
}

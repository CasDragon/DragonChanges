﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using Kingmaker.Blueprints;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;

namespace DragonChanges.BPCoreExtensions
{
    public static class BuffBuilderEx
    {
        /*public static BuffConfigurator AddDRComponent(
            this BuffConfigurator configurator,
            bool? stackable = true,
            ContextValue value = null,
            ContextValue pool = null,
            bool? usePool = false,
            bool? or = false,
            bool? bypassByMaterial = false,
            PhysicalDamageMaterial? material = PhysicalDamageMaterial.Adamantite,
            bool? bypassedByForm = false,
            PhysicalDamageForm? form = PhysicalDamageForm.Bludgeoning,
            bool? bypassedByMagic = false,
            int? minEnhancementBonus = 1,
            bool? bypassedByAlignement = false,
            DamageAlignment? alignment = DamageAlignment.Good,
            bool? bypassedByReality = false,
            DamageRealityType? reality = DamageRealityType.Ghost,
            bool? bypassedByWeaponType = false,
            BlueprintWeaponTypeReference m_weaponType = null,
            bool? bypassedByMeleeWeapon = false,
            bool? bypassedByEpic = false,
            BlueprintUnitFactReference m_CheckedFactMythic = null,
            AddDamageResistancePhysical.WeaponFactFilter weaponFactFilter = AddDamageResistancePhysical.WeaponFactFilter.Any,
            AttackTypeFlag ValidWeaponAttackTypes = (AttackTypeFlag)(-1))
        {
            var element = new AddDamageResistancePhysical();
            element.Value = value ?? element.Value;
            element.UsePool = usePool ?? element.UsePool;
            element.Pool = pool ?? element.Pool;
            element.m_IsStackable = stackable ?? element.m_IsStackable;
            element.Or = or ?? element.Or;
            element.BypassedByMaterial = bypassByMaterial ?? element.BypassedByMaterial;
            element.Material = material ?? element.Material;
            element.BypassedByForm = bypassedByForm ?? element.BypassedByForm;
            element.Form = form ?? element.Form;
            element.BypassedByMagic = bypassedByMagic ?? element.BypassedByMagic;
            element.MinEnhancementBonus = minEnhancementBonus ?? element.MinEnhancementBonus;
            element.BypassedByAlignment = bypassedByAlignement ?? element.BypassedByAlignment;
            element.Alignment = alignment ?? element.Alignment;
            element.BypassedByReality = bypassedByReality ?? element.BypassedByReality;
            element.Reality = reality ?? element.Reality;
            element.BypassedByWeaponType = bypassedByWeaponType ?? element.BypassedByWeaponType;
            element.m_WeaponType = m_weaponType ?? element.m_WeaponType;
            element.BypassedByMeleeWeapon = bypassedByMeleeWeapon ?? element.BypassedByMeleeWeapon;
            element.BypassedByEpic = bypassedByEpic ?? element.BypassedByEpic;
            element.m_CheckedFactMythic = m_CheckedFactMythic ?? element.m_CheckedFactMythic;
            element.ValidWeaponFact = weaponFactFilter;
            element.ValidWeaponAttackTypes = ValidWeaponAttackTypes;
            return configurator.AddComponent(element);
        }*/
    }
}

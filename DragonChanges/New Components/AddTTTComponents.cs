using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Properties;
using TabletopTweaks.Base.MechanicsChanges;
using TabletopTweaks.Core.NewComponents.OwlcatReplacements.DamageResistance;

namespace DragonChanges.New_Components
{
    public static class AddTTTComponents
    {
        public static FeatureConfigurator AddTTAddDamageResistanceHardness(
            this FeatureConfigurator configurator,
            ContextValue value,
            bool stacks = false,
            bool sourceIsArmor = false,
            bool sourceIsClassFeature = false,
            bool usePool = false,
            ContextValue pool = null,
            bool isStackable = false,
            bool isStacksWithFacts = false)
        {
            configurator.AddComponent(new TTAddDamageResistanceHardness()
            {
                Value = value,
                AddToAllStacks = stacks,
                SourceIsArmor = sourceIsArmor,
                SourceIsClassFeature = sourceIsClassFeature,
                UsePool = usePool,
                Pool = pool,
                IsStacksWithArmor = isStackable,
                IsStacksWithClassFeatures = isStacksWithFacts
            });
            return configurator;
        }
        public static FeatureConfigurator AddTTAddDamageResistancePhysical(
            this FeatureConfigurator configurator,
            ContextValue value,
            bool stacks = false,
            bool sourceIsArmor = false,
            bool sourceIsClassFeature = false,
            bool usePool = false,
            ContextValue pool = null, 
            bool isStacksWithArmor = false,
            bool isStacksWithFacts = false,
            bool or = false,
            bool bypassByMaterial = false,
            PhysicalDamageMaterial material = PhysicalDamageMaterial.Adamantite,
            bool bypassedByForm = false,
            PhysicalDamageForm form = PhysicalDamageForm.Bludgeoning,
            bool bypassedByMagic = false,
            int minEnhancementBonus = 1,
            bool bypassedByAlignement = false,
            DamageAlignment alignment = DamageAlignment.Good,
            bool bypassedByReality = false,
            DamageRealityType reality = DamageRealityType.Ghost,
            bool bypassedByWeaponType = false,
            BlueprintWeaponTypeReference m_weaponType = null,
            bool bypassedByMeleeWeapon = false,
            bool bypassedByEpic = false,
            BlueprintUnitFactReference m_CheckedFactMythic = null)
        {
            configurator.AddComponent(new TTAddDamageResistancePhysical()
            {
                Value = value,
                AddToAllStacks = stacks,
                SourceIsArmor = sourceIsArmor,
                SourceIsClassFeature = sourceIsClassFeature,
                UsePool = usePool,
                Pool = pool,
                IsStacksWithArmor = isStacksWithArmor,
                IsStacksWithClassFeatures = isStacksWithFacts,
                Or = or,
                BypassedByMaterial = bypassByMaterial,
                Material = material,
                BypassedByForm = bypassedByForm,
                Form = form,
                BypassedByMagic = bypassedByMagic,
                MinEnhancementBonus = minEnhancementBonus,
                BypassedByAlignment = bypassedByAlignement,
                Alignment = alignment,
                BypassedByReality = bypassedByReality,
                Reality = reality,
                BypassedByWeaponType = bypassedByWeaponType,
                m_WeaponType = m_weaponType,
                BypassedByMeleeWeapon = bypassedByMeleeWeapon,
                BypassedByEpic = bypassedByEpic,
                m_CheckedFactMythic = m_CheckedFactMythic
            });
            return configurator;
        }
        public static FeatureConfigurator AddTTAddDamageResistancePhysicalNew(
            this FeatureConfigurator configurator,
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
            TTAddDamageResistancePhysical newElement = (TTAddDamageResistancePhysical)DRRework.BlueprintFact_CollectComponents_Patch.CreateFromVanillaDamageResistance(element);
            newElement.AddToAllStacks = true;
            return configurator.AddComponent(newElement);
        }
        public static FeatureConfigurator AddTTAddDamageResistancePhysicalTest(
            this FeatureConfigurator configurator,
            ContextValue value = null,
            bool? stackable =  null)
        {
            AddDamageResistancePhysical component = TTTHelpers.CreateCopy(BuffRefs.ArmorFocusHeavyMythicFeatureVar1SubBuff.Reference.Get().GetComponent<AddDamageResistancePhysical>());
            component.Value = value ??  component.Value;
            return configurator.AddComponent(component);
        }
    }
}

using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Mechanics;
using TabletopTweaks.Core.NewComponents.OwlcatReplacements.DamageResistance;

namespace DragonChanges.New_Components
{
    internal class AddTTTComponents
    {
        public static void AddTTAddDamageResistanceHardness(
            FeatureConfigurator configurator,
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
        }
        public static void AddTTAddDamageResistancePhysical(
            FeatureConfigurator configurator,
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
        }
    }
}

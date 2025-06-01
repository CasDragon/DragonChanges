using BlueprintCore.Blueprints.CustomConfigurators.Classes;
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
            bool isStackable = false,
            bool isStacksWithFacts = false)
        {
            configurator.AddComponent(new TTAddDamageResistancePhysical()
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
    }
}

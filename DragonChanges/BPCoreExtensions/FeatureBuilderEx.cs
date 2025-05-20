using BlueprintCore.Blueprints.Configurators.Facts;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using DragonChanges.New_Components;
using DragonChanges.NewItems;
using DragonChanges.NewStuff;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Mechanics;

namespace DragonChanges.BPCoreExtensions
{
    public static class FeatureBuilderEx
    {
        public static FeatureConfigurator AddWorkingAttackStatReplacementForWeaponGroup(
            this FeatureConfigurator configurator,
            StatType? ReplacementStat = StatType.Charisma,
            WeaponFighterGroupFlags? FighterGroupFlag = WeaponFighterGroupFlags.Natural)
        {
            var element = new AttackStatReplacementForWeaponGroup();
            element.ReplacementStat = ReplacementStat ?? element.ReplacementStat;
            element.FighterGroupFlag = FighterGroupFlag ?? element.FighterGroupFlag;
            return configurator.AddComponent(element);
        }
        public static FeatureConfigurator AddDRComponent(
            this FeatureConfigurator configurator,
            StatType? stat = StatType.Charisma,
            bool? stackable = true,
            int? multiplier = 1)
        {
            var element = new DRComponent();
            element.Stat = stat ?? element.Stat;
            element.m_IsStackable = stackable ?? element.m_IsStackable;
            element.Multiplier = multiplier ?? element.Multiplier;
            return configurator.AddComponent(element);
        }
        public static FeatureConfigurator AddNewDRComponent(
            this FeatureConfigurator configurator,
            bool? stackable = true,
            ContextValue? value = null,
            DamageEnergyTypeFlag? excludedTypes = null,
            bool? objectValue = null,
            ContextValue pool = null,
            bool? usePool = null)
        {
            var element = new DRComponentNew();
            element.stackable = stackable ?? element.stackable;
            element.Value = value ?? element.Value;
            element.m_ExcludedTypes = excludedTypes ?? element.m_ExcludedTypes;
            element.m_Object = objectValue ?? element.m_Object;
            element.Pool = pool ?? element.Pool;
            element.UsePool = usePool ?? element.UsePool;
            return configurator.AddComponent(element);
        }
    }
}

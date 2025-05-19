using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using DragonChanges.NewStuff;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.EntitySystem.Stats;

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Mechanics;

namespace DragonChanges.Content
{
    internal class InsightfulContemplationChanges
    {
        const string settingName = "cyrix-wis-buff";
        const string settingDescription = "Adds a WIS buff to Insightful Contemplation, added for Cyrix";
        [DragonConfigure]
        [DragonSetting(settingCategories.Various, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
            {
                Main.log.Log("Patching InsightfulComtemplationSngEffectBuff to include WIS buff");
                BuffConfigurator.For(BuffRefs.InsightfulContemplationSongEffectBuff)
                    .AddContextStatBonus(StatType.Wisdom,
                        new ContextValue()
                        {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = Kingmaker.Enums.AbilityRankType.Default,
                            ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Damage,
                            Property = Kingmaker.UnitLogic.Mechanics.Properties.UnitProperty.None
                        },
                        Kingmaker.Enums.ModifierDescriptor.Morale,
                        0,
                        2)
                    .SetDescription("InsightfulContemplation.description")
                    .Configure();
                BuffConfigurator.For(BuffRefs.InsightfulContemplationSongEffectBuffMythic)
                    .AddContextStatBonus(StatType.Wisdom,
                        new ContextValue()
                        {
                            ValueType = ContextValueType.Shared,
                            Value = 0,
                            ValueRank = Kingmaker.Enums.AbilityRankType.Default,
                            ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Heal,
                            Property = Kingmaker.UnitLogic.Mechanics.Properties.UnitProperty.None,
                            PropertyName = Kingmaker.Enums.ContextPropertyName.Value1,
                            m_AbilityParameter = AbilityParameterType.Level
                        },
                        Kingmaker.Enums.ModifierDescriptor.Morale,
                        0,
                        1,
                        new Kingmaker.Designers.Mechanics.Facts.Restrictions.RestrictionCalculator()
                        {
                            Property = new Kingmaker.EntitySystem.Properties.PropertyCalculator()
                            {
                                Operation = Kingmaker.EntitySystem.Properties.PropertyCalculator.OperationType.Sum,
                                TargetType = Kingmaker.EntitySystem.Properties.PropertyTargetType.CurrentEntity
                            }
                        })
                    .Configure();
            }
        }
    }
}

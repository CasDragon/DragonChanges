using BlueprintCore.Blueprints.Configurators.UnitLogic.Properties;
using DragonChanges.Utils;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace DragonChanges.NewStuff.Feats.Buffs;

internal static class BackdownProperty
{
    // edit
    internal const string feature = "BackdownProperty";
    internal const string featureguid = Guids.BackdownProperty;
    
    public static void ConfigureDummy()
    {
        UnitPropertyConfigurator.New(feature, featureguid)
            .Configure();
    }

    public static BlueprintUnitProperty ConfigureEnabled()
    {
        var x = UnitPropertyConfigurator.New(feature, featureguid)
            .SetOperationOnComponents(BlueprintUnitProperty.MathOperation.Sum)
            .SetBaseValue(10)
            .AddStatValueGetter(stat: StatType.Strength, valueType: StatValueGetter.ReturnType.Bonus,
                settings: new PropertySettings(){m_Progression = PropertySettings.Progression.AsIs})
            .AddSkillValueGetter(skill: StatType.CheckIntimidate,
                settings: new PropertySettings(){m_Progression = PropertySettings.Progression.AsIs})
            .AddSimplePropertyGetter(UnitProperty.Level, new PropertySettings(){m_Progression = PropertySettings.Progression.Div2})
            .Configure();
        return x;
    }
}
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.MiscEx;
using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;
using Kingmaker.Localization;
using UnityEngine;

namespace DragonChanges.NewStuff;

public class LannPet
{
    internal const string unitBPName = "lannpetunit";
    [DragonLocalizedString(unitname, "Lann The Pet")]
    internal const string unitname = "lannpetunit.name";

    public static void ConfigureDisabled()
    {
        UnitConfigurator.New(unitBPName, Guids.LannPetUnit).Configure();
    }

    public static BlueprintUnit ConfigureEnabled()
    {
        var aivu = TTTHelpers.CreateCopy<BlueprintUnit>(UnitRefs.Lann_Companion.Reference.Get());
        var shared = ScriptableObject.CreateInstance<SharedStringAsset>();
        shared.String = LocalizationTool.GetString(unitname);
        var unit = UnitConfigurator.New(unitBPName, Guids.LannPetUnit)
            .CopyFrom(aivu, typeof(AddClassLevels))
            .AddClassLevelLimit(1)
            .AddMythicLevelLimit(0)
            .AddAllowDyingCondition()
            .AddResurrectOnRest()
            .SetLocalizedName(shared)
            .SetGender(aivu.Gender)
            .SetSize(aivu.Size)
            .SetAlignment(aivu.Alignment)
            .SetPortrait(aivu.PortraitSafe)
            .SetPrefab(aivu.Prefab)
            .SetVisual(aivu.Visual)
            .SetFaction(aivu.Faction)
            .SetBody(aivu.Body)
            .SetStrength(aivu.Strength)
            .SetIntelligence(aivu.Intelligence)
            .SetDexterity(aivu.Dexterity)
            .SetConstitution(aivu.Constitution)
            .SetWisdom(aivu.Wisdom)
            .SetCharisma(aivu.Charisma)
            .SetSpeed(aivu.Speed)
            .SetMaxHP(aivu.MaxHP)
            .SetSkills(aivu.Skills)
            .Configure();
        
        return unit;
    }
}
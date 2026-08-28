using BlueprintCore.Blueprints.CustomConfigurators.Classes.Spells;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.ModRefs;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;

namespace DragonChanges.Content;

public class CharacterOptionPlus
{
    [DragonConfigure]
    public static void DimBlade()
    {
        if (!ModCompat.cop)
            return;
        Main.log.Log("Adding Dimensional Blade to Warpriest spell list");
        BlueprintAbilityReference x = new() {deserializedGuid = BlueprintGuid.Parse(CharacterOptionsPlusRefs.DimensionalBlade)};
        SpellListConfigurator.For(SpellListRefs.WarpriestSpelllist)
            .AddToSpellsByLevel(new SpellLevelList(6) { m_Spells = [x] })
            .Configure();
    }
}
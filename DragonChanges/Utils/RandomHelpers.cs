using BlueprintCore.Blueprints.CustomConfigurators.Classes.Spells;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;

namespace DragonChanges.Utils;

public class RandomHelpers
{
    private static readonly Blueprint<BlueprintReference<BlueprintSpellList>>[] thass = [
        SpellListRefs.ThassilonianTransmutationSpellList, SpellListRefs.ThassilonianAbjurationSpellList,
        SpellListRefs.ThassilonianConjurationSpellList, SpellListRefs.ThassilonianEnchantmentSpellList,
        SpellListRefs.ThassilonianEvocationSpellList, SpellListRefs.ThassilonianIllusionSpellList,
        SpellListRefs.ThassilonianNecromancySpellList
    ];

    public static void AddToThassilonian(BlueprintAbility spell, int level)
    {
        var x = new SpellLevelList(level);
        x.m_Spells.Add(spell.ToReference<BlueprintAbilityReference>());
        foreach (var thas in thass)
            SpellListConfigurator.For(thas)
                .AddToSpellsByLevel(x)
                .Configure();
    }
}
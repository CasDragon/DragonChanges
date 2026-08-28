using BlueprintCore.Blueprints.CustomConfigurators.Classes.Spells;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;

namespace DragonChanges.Content;

public class PrestigePlus
{
    [DragonConfigure]
    public static void BladeLash()
    {
        if (!ModCompat.pp)
            return;
        Main.log.Log("Adding Blade Lash to Warpriest/Inquist spell list");
        BlueprintAbilityReference x = new() {deserializedGuid = BlueprintGuid.Parse(OtherModGuids.UseBladeLash)};
        SpellListConfigurator.For(SpellListRefs.WarpriestSpelllist)
            .AddToSpellsByLevel(new SpellLevelList(1) { m_Spells = [x] })
            .Configure();
        SpellListConfigurator.For(SpellListRefs.InquisitorSpellList)
            .AddToSpellsByLevel(new SpellLevelList(1) { m_Spells = [x] })
            .Configure();
    }
}
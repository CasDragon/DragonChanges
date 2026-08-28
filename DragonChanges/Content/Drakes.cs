using BlueprintCore.Blueprints.Configurators;
using DragonChanges.Utils;
using DragonLibrary.ModRefs;
using DragonLibrary.Utils;

namespace DragonChanges.Content
{
    internal class Drakes
    {
        const string settingName = "ec-drakes";
        const string settingDescription = "Super secret option to buff drakes from Expanded Content";
        [DragonConfigure]
        [DragonSetting(SettingCategories.ModCompatability, settingName, settingDescription, false)]
        public static void PatchDrakes()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                if (ModCompat.expandedcontent)
                {
                    Main.log.Log("Patching drakes with meme op stats");
                    string[] drakes = [ExpandedContentRefs.DrakeCompanionUnitBlack,  ExpandedContentRefs.DrakeCompanionUnitWhite,
                        ExpandedContentRefs.DrakeCompanionUnitBlue, ExpandedContentRefs.DrakeCompanionUnitBrass,
                        ExpandedContentRefs.DrakeCompanionUnitBronze, ExpandedContentRefs.DrakeCompanionUnitCopper,
                        ExpandedContentRefs.DrakeCompanionUnitGold, ExpandedContentRefs.DrakeCompanionUnitGreen,
                        ExpandedContentRefs.DrakeCompanionUnitRed, ExpandedContentRefs.DrakeCompanionUnitSilver,
                        ExpandedContentRefs.DrakeCompanionUnitUmbral];
                    foreach (string drake in drakes)
                    {
                        UnitConfigurator.For(drake)
                            .SetStrength(40)
                            .SetDexterity(40)
                            .SetConstitution(40)
                            .SetIntelligence(20)
                            .SetWisdom(40)
                            .SetCharisma(40)
                            .Configure();
                    }
                }
                else
                {
                    Main.log.Log("Meme drake option enabled, but EC isn't detected, skipping patches");
                }
            }
        }
    }
}

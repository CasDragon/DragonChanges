using BlueprintCore.Blueprints.Configurators;
using DragonChanges.Utils;

namespace DragonChanges.Content
{
    internal class Drakes
    {
        const string settingName = "ec-drakes";
        const string settingDescription = "Super secret option to buff drakes from Expanded Content";
        [DragonConfigure]
        [DragonSetting(settingCategories.ModCompatability, settingName, settingDescription, false)]
        public static void PatchDrakes()
        {
            if (NewSettings.GetSetting<bool>(settingName))
            {
                if (ModCompat.expandedcontent)
                {
                    Main.log.Log("Patching drakes with meme op stats");
                    // black
                    UnitConfigurator.For("9618870effa94c348acd8b6763f7dca0")
                        .SetStrength(40)
                        .SetDexterity(40)
                        .SetConstitution(40)
                        .SetIntelligence(30)
                        .SetWisdom(40)
                        .SetCharisma(40)
                        .Configure();
                    // blue
                    UnitConfigurator.For("bde018e76d424a739bcd52a777eb3bd2")
                        .SetStrength(40)
                        .SetDexterity(40)
                        .SetConstitution(40)
                        .SetIntelligence(30)
                        .SetWisdom(40)
                        .SetCharisma(40)
                        .Configure();
                    // brass
                    UnitConfigurator.For("c70218576aab40f8a601ebaf86bea776")
                        .SetStrength(40)
                        .SetDexterity(40)
                        .SetConstitution(40)
                        .SetIntelligence(30)
                        .SetWisdom(40)
                        .SetCharisma(40)
                        .Configure();
                    // bronze
                    UnitConfigurator.For("9329050b3f444846a85edfea7e7bff77")
                        .SetStrength(40)
                        .SetDexterity(40)
                        .SetConstitution(40)
                        .SetIntelligence(30)
                        .SetWisdom(40)
                        .SetCharisma(40)
                        .Configure();
                    // copper
                    UnitConfigurator.For("38aaa7789e3343ec9822d00102c11d47")
                        .SetStrength(40)
                        .SetDexterity(40)
                        .SetConstitution(40)
                        .SetIntelligence(30)
                        .SetWisdom(40)
                        .SetCharisma(40)
                        .Configure();
                    // gold
                    UnitConfigurator.For("9422eb0ef662499da6a596f85ebeb6e7")
                        .SetStrength(40)
                        .SetDexterity(40)
                        .SetConstitution(40)
                        .SetIntelligence(30)
                        .SetWisdom(40)
                        .SetCharisma(40)
                        .Configure();
                    // green
                    UnitConfigurator.For("9c824cba62cc4030beace1545a0083fc")
                        .SetStrength(40)
                        .SetDexterity(40)
                        .SetConstitution(40)
                        .SetIntelligence(30)
                        .SetWisdom(40)
                        .SetCharisma(40)
                        .Configure();
                    // red 
                    UnitConfigurator.For("47912a3f25be4219ab71fdd8f64ee5ab")
                        .SetStrength(40)
                        .SetDexterity(40)
                        .SetConstitution(40)
                        .SetIntelligence(30)
                        .SetWisdom(40)
                        .SetCharisma(40)
                        .Configure();
                    // silver
                    UnitConfigurator.For("6af628a8a7f3479987551e30e178c090")
                        .SetStrength(40)
                        .SetDexterity(40)
                        .SetConstitution(40)
                        .SetIntelligence(30)
                        .SetWisdom(40)
                        .SetCharisma(40)
                        .Configure();
                    // umbral
                    UnitConfigurator.For("7b0c9f1ce01b4ab084e571a2c88e9f67")
                        .SetStrength(40)
                        .SetDexterity(40)
                        .SetConstitution(40)
                        .SetIntelligence(30)
                        .SetWisdom(40)
                        .SetCharisma(40)
                        .Configure();
                    // white 
                    UnitConfigurator.For("73d326dd3c054d4faca67c589ffbf198")
                        .SetStrength(40)
                        .SetDexterity(40)
                        .SetConstitution(40)
                        .SetIntelligence(30)
                        .SetWisdom(40)
                        .SetCharisma(40)
                        .Configure();
                }
                else
                {
                    Main.log.Log("Meme drake option enabled, but EC isn't detected, skipping patches");
                }
            }
        }
    }
}

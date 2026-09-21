using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Buffs;

namespace DragonChanges.Content;

public class AivuModel
{
    [DragonConfigure]
    [DragonSetting(SettingCategories.Various, "aivumodel", "Change Copper Dragonblood shifter model to Aivu", false)]
    public static void DoIt()
    {
        if (SettingsAction.GetSetting<bool>("aivumodel"))
        {
            Main.log.Log("Changing Copper Dragonblood shifter model to Aivu");
            var prefab = UnitRefs.AzataDragonUnit.Reference.Get().Prefab;
            BuffConfigurator.For(BuffRefs.ShifterDragonFormCopperBuff9)
                .EditComponent<Polymorph>(c => c.m_Prefab = prefab)
                .Configure();
            BuffConfigurator.For(BuffRefs.ShifterDragonFormCopperBuff14)
                .EditComponent<Polymorph>(c => c.m_Prefab = prefab)
                .Configure();
            BuffConfigurator.For(BuffRefs.ShifterDragonFormCopperBuff20)
                .EditComponent<Polymorph>(c => c.m_Prefab = prefab)
                .Configure();
        }
    }
}
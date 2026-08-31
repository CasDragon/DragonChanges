using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Kingmaker.Modding;

namespace DragonChanges.Patches;

[HarmonyPatch(typeof(OwlcatModificationsManager))]
public static class OwlmodPatch
{
    private static bool Initialized = false;
            
    [HarmonyPatch(nameof(OwlcatModificationsManager.LoadModifications), new Type[] {  })]
    [HarmonyPostfix]
    public static void LoadModifications_Patch(OwlcatModificationsManager __instance)
    {
        if (Initialized) return;
        Initialized = true;
        string modfolder = new FileInfo(Main.entry.Assembly.Location).Directory!.FullName;
        var path = Path.Combine(modfolder, "WoolooBundle");
        OwlcatModification owlcatModification = OwlcatModification
            .LoadFromDirectory(path, path);
        __instance.m_Modifications.AddItem(owlcatModification);
        foreach (var mod in __instance.m_Modifications)
        {
            Main.log.Log($"Owlmods - {mod.Manifest.UniqueName}");
        }
    }
}

[HarmonyPatch(typeof(OwlcatModificationsManager), "ApplyModifications")]
public class WoolooLoadingPatch
{
    
    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);

        FieldInfo enabledModsField = AccessTools.Field(
            AccessTools.Inner(typeof(OwlcatModificationsManager), "SettingsData"),
            "EnabledModifications");

        bool patched = false;

        for (int i = 1; i < codes.Count; i++)
        {
            // Match: ldfld EnabledModifications -> stloc.1
            if (codes[i].opcode == OpCodes.Stloc_1 &&
                codes[i - 1].opcode == OpCodes.Ldfld &&
                ReferenceEquals(codes[i - 1].operand, enabledModsField))
            {
                int insertAt = i + 1;

                codes.InsertRange(insertAt, new[]
                {
                    new CodeInstruction(OpCodes.Ldloc_1),   // load enabledModifications
                    new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(WoolooLoadingPatch), nameof(OnEnabledModificationsLoaded))),
                    new CodeInstruction(OpCodes.Stloc_1),   // enabledModifications = result
                });

                patched = true;
                break;
            }
        }

        if (!patched)
        {
            throw new Exception("ApplyModifications_Patch: could not find EnabledModifications store point to patch.");
        }

        return codes;
    }

    // enabledModifications = OnEnabledModificationsLoaded(enabledModifications);
    static string[] OnEnabledModificationsLoaded(string[] enabledModifications)
    {
        string[] newlist = [.. enabledModifications, "WoolooMod"];
        Main.log.Log($"EnabledModifications loaded: {string.Join(", ", newlist)}");
        return newlist;
    }
}

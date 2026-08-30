using BlueprintCore.Utils;
using DragonChanges.Utils;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.PubSubSystem;
using UnityEngine;

namespace DragonChanges.NewStuff.VendorStuff;

public class VendorSpawner: IAreaActivationHandler
{
    public void OnAreaActivated()
    {
        Main.log.Log("New area loaded");
        Main.log.Log($"Current Area - {Game.Instance.CurrentlyLoadedArea.AssetGuid.ToString()}");
        if (Game.Instance.CurrentlyLoadedAreaPart != null)
            Main.log.Log($"Current Area Part - {Game.Instance.CurrentlyLoadedAreaPart.AssetGuid.ToString()}");
        Main.log.Log("New area loaded - End");
        
        switch (Game.Instance.CurrentlyLoadedArea.AssetGuid.ToString())
        {
            case "9771d911f03440ce819b1ef4829169bf": // DLC3
                SpawnTheVendor(new Vector3(-13.5f, 8.6f, -1.6f));
                break;
            case "7a25c101fe6f7aa46b192db13373d03b": // Act 2
                SpawnTheVendor(new Vector3(19.7f, 40.0f, 35.4f));
                break;
            case "180cdb4b48d561f4cb4ef9a066727960": // Act 4
                if (Game.Instance.CurrentlyLoadedAreaPart!.AssetGuid == "27b0684aedfca0a4ca1eb437e77abb3f")
                    SpawnTheVendor(new Vector3(110.1f, 3.41f, 89.04f));
                break;
            case "2570015799edf594daf2f076f2f975d8": // Act 3 & 5?
                if (Game.Instance.CurrentlyLoadedAreaPart!.AssetGuid == "8a076e720870a44438d13b9b939933fd")
                    SpawnTheVendor(new Vector3(39.9f, 56.0f, 8.3f));
                break;
            case "089e897983fef564d9e15b46ff679d7e": // Act 1
                if (Game.Instance.CurrentlyLoadedAreaPart!.AssetGuid == "089e897983fef564d9e15b46ff679d7e")
                    SpawnTheVendor(new Vector3(39.1f, 49.4f, 10.2f));
                break;
            default:
                return;
        }
    }

    public void SpawnTheVendor(Vector3 position)
    {
        foreach (var unit in Game.Instance.State.Units)
        {
            if (unit.Blueprint.AssetGuid.ToString() == Guids.DLCVendorUnit)
            {
                VendorUnit.isVendorSpawned = true;
                break;
            }
        }

        if (VendorUnit.isVendorSpawned) return;
        var unitSpawned = Game.Instance.EntityCreator.SpawnUnit(BlueprintTool.Get<BlueprintUnit>(Guids.DLCVendorUnit),
            position, Quaternion.identity, Game.Instance.State.LoadedAreaState.MainState);
        VendorUnit.isVendorSpawned = true;
    }
}
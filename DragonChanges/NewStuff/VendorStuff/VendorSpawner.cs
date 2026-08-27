using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Experience;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using TabletopTweaks.Core.Utilities;
using UnityEngine;

namespace DragonChanges.NewStuff.VendorStuff;

public class VendorSpawner: IAreaHandler
{
    
    public void OnAreaBeginUnloading()
    {
        //throw new NotImplementedException();
    }

    public void OnAreaDidLoad()
    {
        /*Main.log.Log("New area loaded");
        Main.log.Log($"Current Area - {Game.Instance.CurrentlyLoadedArea.AssetGuid.ToString()}");
        if (Game.Instance.CurrentlyLoadedAreaPart != null)
            Main.log.Log($"Current Area Part - {Game.Instance.CurrentlyLoadedAreaPart.AssetGuid.ToString()}");
        Main.log.Log("New area loaded - End");*/
        if (Game.Instance.CurrentlyLoadedArea.AssetGuid.ToString() != "9771d911f03440ce819b1ef4829169bf")
            return;
        Main.log.Log("In DLC3 area");
        foreach (var unit in Game.Instance.State.Units)
        {
            if (unit.Blueprint.AssetGuid.ToString() == Guids.DLCVendorUnit)
            {
                VendorUnit.isVendorSpawned = true;
                break;
            }
        }

        if (VendorUnit.isVendorSpawned) return;
        Main.log.Log("Spawning DLC vendor");
        var unitSpawned = Game.Instance.EntityCreator.SpawnUnit(BlueprintTool.Get<BlueprintUnit>(Guids.DLCVendorUnit),
            new Vector3(-13.5f, 8.6f, -1.6f), Quaternion.identity, 
            Game.Instance.State.LoadedAreaState.MainState);
        //Main.log.Log($"Unit got spawned at {unitSpawned.Position.ToString()}");
        VendorUnit.isVendorSpawned = true;
    }
}
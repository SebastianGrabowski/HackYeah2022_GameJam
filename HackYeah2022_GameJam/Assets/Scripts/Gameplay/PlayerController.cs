using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public WorldTile TileHovered;

    private void Update()
    {
        if(TileHovered != null && Input.GetMouseButtonDown(0) && !Game.BuildWindow.LockMapClick) 
        {
            if(TileHovered.Building != null && TileHovered.Building.ProcessReady)
            {
                TileHovered.Building.AddProcessAction();
            } else
            {
                if (TileHovered.CanCollect())
                {
                    Collect();
                } else if (TileHovered.TileType == TileType.Building)
                {
                    if(TileHovered.Building.ActiveBuildingID == -1)
                    {
                        Game.GameplayView.Instance.OpenBuildWindow(TileHovered);
                    }
                }
            }
        }
    }

    private void Collect()
    {
        //Add collectible amount;
        var collectible = TileHovered.CollectibleObj.GetComponent<Collectible>();
        collectible.Collect();
    }
}

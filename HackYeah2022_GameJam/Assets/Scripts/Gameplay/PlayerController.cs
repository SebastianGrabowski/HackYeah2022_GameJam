using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public WorldTile TileHovered;

    private void Update()
    {
        if(TileHovered != null && Input.GetMouseButtonDown(0)) 
        {
            if (TileHovered.CanCollect())
            {
                Collect();
            } else if (TileHovered.TileType == TileType.Building)
            {
                Game.GameplayView.Instance.OpenBuildWindow(TileHovered);
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

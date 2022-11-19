using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public WorldTile TileHovered;

    private void Update()
    {
        if(TileHovered != null && TileHovered.CanCollect() && Input.GetMouseButtonDown(0)) 
        {
            Collect();
        }
    }

    private void Collect()
    {
        //Add collectible amount;
        var collectible = TileHovered.CollectibleObj.GetComponent<Collectible>();
        Game.Gameplay.GameplayController.Instance.Resources[collectible.ResourceData.ID] += collectible.Amount;
        Destroy(TileHovered.CollectibleObj);
    }
}

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
            if (Game.Gameplay.GameplayController.Instance.DestroyBuildingMode)
            {
                if(TileHovered.Building != null && TileHovered.Building.ActiveBuildingID != -1 && TileHovered.Building.Build)
                {
                    
                    Game.Main.Instance.DestroySFX.Play();
                    Game.Gameplay.GameplayController.Instance.DestroyHandler(TileHovered.Building._Data);
                    TileHovered.Building.DestroyHandler();
                    TileHovered.DestroyHandler();
                    TileHovered.Building.Set(-1);
                    TileHovered.CollectibleObj = null;
                }
            } else
            {
                if(TileHovered.Building != null && TileHovered.Building.ProcessReady)
                {
                    TileHovered.Building.AddProcessAction();
                } else
                {
                    if (TileHovered.CanCollect())
                    {

                        if(TileHovered.ic == IndicatorIconType.WheatCollect)
                            Game.Main.Instance.ScytheSFX.Play();
        
                        if(TileHovered.ic == IndicatorIconType.WoodCollect)
                            Game.Main.Instance.ScytheSFX.Play();
        
                        if(TileHovered.ic == IndicatorIconType.WoolCollect)
                            Game.Main.Instance.SheepSFX.Play();
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
    }

    private void Collect()
    {
        //Add collectible amount;
        var collectible = TileHovered.CollectibleObj.GetComponent<Collectible>();
        collectible.Collect();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Land,
    Farm,
    Building,
    Mountains
}

public class WorldTile : MonoBehaviour
{
    [HideInInspector] public GameObject CollectibleObj;
    [HideInInspector] public Game.Gameplay.Building Building;


    public TileType TileType;
    private Indicator _Indicator;
    private PlayerController _PlayerController;
    
    private void Awake()
    {
        _Indicator = FindObjectOfType<Indicator>();
        _PlayerController = FindObjectOfType<PlayerController>();
        Building = GetComponent<Game.Gameplay.Building>();
    }

    private void OnMouseOver()
    {
        _PlayerController.TileHovered = this;
        if(TileType != TileType.Mountains) _Indicator.UpdateIndicatorPosition(this.transform.position);
    }

    private void OnMouseExit()
    {
        _PlayerController.TileHovered = null;
        _Indicator.SetActive(false);
    }

    public bool CanCollect()
    {
        if(CollectibleObj != null) return CollectibleObj.GetComponent<Collectible>().CanCollect();

        return false;
    }

    public void Build(int buildingDataID)
    {
        Building.Set(buildingDataID);
    }
}

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
    [SerializeField] private GameObject _WheatCollectibleObj;
    [SerializeField] private GameObject _SheepCollectibleObj;

    [HideInInspector] public GameObject CollectibleObj;
    [HideInInspector] public Game.Gameplay.Building Building;
    [SerializeField]private GameObject _RandomObj;



    public TileType TileType;
    private Indicator _Indicator;
    private PlayerController _PlayerController;
    
    private void Awake()
    {
        if (_RandomObj != null)
            _RandomObj.gameObject.SetActive(Random.Range(0, 100) > 70);
        _Indicator = FindObjectOfType<Indicator>();
        _PlayerController = FindObjectOfType<PlayerController>();
        Building = GetComponent<Game.Gameplay.Building>();
    }

    private void OnMouseOver()
    {
        _PlayerController.TileHovered = this;
        IndicatorIconType indicatorIconType = IndicatorIconType.None;

        if(CollectibleObj != null && CanCollect()) 
        {
            if(CollectibleObj.GetComponent<Collectible>().CollectibleType == CollectibleType.Wood) indicatorIconType = IndicatorIconType.WoodCollect;
            else if(CollectibleObj.GetComponent<Collectible>().CollectibleType == CollectibleType.Wood) indicatorIconType = IndicatorIconType.WoolCollect;
            else if(CollectibleObj.GetComponent<Collectible>().CollectibleType == CollectibleType.Wheat) indicatorIconType = IndicatorIconType.WheatCollect;
        }

        if(Building != null && Building.ActiveBuildingID != -1 && Game.Gameplay.GameplayController.Instance.DestroyBuildingMode) indicatorIconType = IndicatorIconType.BuildingDestroy;
        else if(Building != null && Building.ActiveBuildingID != -1 && !Game.Gameplay.GameplayController.Instance.DestroyBuildingMode) 
        {
            if(Building.ActiveBuildingID == 5) indicatorIconType = IndicatorIconType.WoolCollect;
            else if(Building.ActiveBuildingID == 0 || Building.ActiveBuildingID == 4) indicatorIconType = IndicatorIconType.WheatCollect;
        }
        else if(CollectibleObj == null) indicatorIconType = IndicatorIconType.Build;
        
        if(TileType != TileType.Mountains) _Indicator.UpdateIndicatorPosition(this.transform.position, indicatorIconType);
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
        
        if(Building.ActiveBuildingID == 4) CollectibleObj = _WheatCollectibleObj;
        else if(Building.ActiveBuildingID == 5) CollectibleObj = _SheepCollectibleObj;
    }
}

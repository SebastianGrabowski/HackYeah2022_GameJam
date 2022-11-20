using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjectData
{
    public TileType TileType;
    public GameObject Object;
    public float Amount;
}

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private float _TreeRespawnTime;

    [SerializeField] private int _MaxTreesAtOnce;
    [SerializeField] private int _MinTreesAmount;

    [SerializeField] private ObjectData[] _ObjectData;
    [SerializeField] private TileGridCreator[] _TileGridCreators;

    private List<WorldTile> _AvaiableTiles = new List<WorldTile>();
    private float _TimeToElapse = 0f;
    private int _CurrentTreesAmount;

    void Update()
    {
        CheckCurrenTreesCount();
        if(_CurrentTreesAmount <= _MinTreesAmount && Time.time >= _TimeToElapse)
        {
            SpawnMoreTrees();
        }
    }

    private void Start()
    {
        SetAvaiableTiles();

        for(int i = 0; i < _ObjectData.Length; i++)
        {
            if(!HasAvaiableTile(_AvaiableTiles, _ObjectData[i].TileType)) continue;

            var amount = _ObjectData[i].Amount;
            while(amount > 0)
            {
                var randomTile = Random.Range(0, _AvaiableTiles.Count);
                
                if(_AvaiableTiles[randomTile].TileType == _ObjectData[i].TileType)
                {
                    var obj = Instantiate(_ObjectData[i].Object, _AvaiableTiles[randomTile].transform.position, Quaternion.identity);
                    
                    var collectible = _ObjectData[i].Object.GetComponent<Collectible>();
                    if(collectible != null)
                    {
                        if(collectible.CollectibleType == CollectibleType.Wood) _CurrentTreesAmount++;
                        _AvaiableTiles[randomTile].CollectibleObj = obj;
                    }
                    
                    _AvaiableTiles.Remove(_AvaiableTiles[randomTile]);
                    amount --;
                }
            }
        }
    }

    private void SetAvaiableTiles()
    {
        _AvaiableTiles = new List<WorldTile>();
        foreach(var tileGrid in _TileGridCreators)
        {
            foreach(var tile in tileGrid.WorldTiles)
            {
                if(tile.Building.ActiveBuildingID == -1 && tile.CollectibleObj == null) _AvaiableTiles.Add(tile);
            }
        }
    }

    private bool HasAvaiableTile(List<WorldTile> totalTiles, TileType tileType)
    {
        foreach(var tile in totalTiles)
        {
            if(tile.TileType == tileType) return true;
        }

        return false;
    }

    private void CheckCurrenTreesCount()
    {
        foreach(var tile in _AvaiableTiles)
        {
            var collectible = tile.GetComponent<Collectible>();
            if(collectible != null && collectible.CollectibleType == CollectibleType.Wood)
            {
                _CurrentTreesAmount++;
            }
        }
    }

    private void SpawnMoreTrees()
    {
        SetAvaiableTiles();

        //if(!HasAvaiableTile(_AvaiableTiles, TileType.Land)) return;

        var amount = 1;
        while(amount > 0)
        {
            var randomTile = Random.Range(0, _AvaiableTiles.Count);
            var obj = Instantiate(_ObjectData[0].Object, _AvaiableTiles[randomTile].transform.position, Quaternion.identity);
            
            var collectible = _ObjectData[0].Object.GetComponent<Collectible>();
            if(collectible != null)
            {
                if(collectible.CollectibleType == CollectibleType.Wood) _CurrentTreesAmount++;
                _AvaiableTiles[randomTile].CollectibleObj = obj;
            }
            
            _AvaiableTiles.Remove(_AvaiableTiles[randomTile]);
            amount --;
        }

        _TimeToElapse = Time.time + _TreeRespawnTime;
    }

    public void OnTreeCollected()
    {
        SetAvaiableTiles();
        _CurrentTreesAmount--;
        if(_CurrentTreesAmount < 0) _CurrentTreesAmount = 0;
    }
}

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
    [SerializeField] private ObjectData[] _ObjectData;
    [SerializeField] private TileGridCreator[] _TileGridCreators;

    private void Start()
    {
        var totalTiles = new List<WorldTile>();
        foreach(var tileGrid in _TileGridCreators)
        {
            foreach(var tile in tileGrid.WorldTiles)
            {
                totalTiles.Add(tile);
            }
        }


        for(int i = 0; i < _ObjectData.Length; i++)
        {
            if(!HasAvaiableTile(totalTiles, _ObjectData[i].TileType)) continue;

            var amount = _ObjectData[i].Amount;
            while(amount > 0)
            {
                var randomTile = Random.Range(0, totalTiles.Count);
                
                if(totalTiles[randomTile].TileType == _ObjectData[i].TileType)
                {
                    var obj = Instantiate(_ObjectData[i].Object, totalTiles[randomTile].transform.position, Quaternion.identity);
                    
                    var collectible = _ObjectData[i].Object.GetComponent<Collectible>();
                    if(collectible != null) totalTiles[randomTile].CollectibleObj = obj;
            
                    totalTiles.Remove(totalTiles[randomTile]);
                    amount --;
                }
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
}

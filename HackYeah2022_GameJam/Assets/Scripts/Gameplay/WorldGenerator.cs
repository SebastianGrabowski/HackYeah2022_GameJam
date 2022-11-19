using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private bool _IsCollectible;
    [SerializeField] private float _ObjectsAmount;

    [SerializeField] private TileType _TileType;

    [SerializeField] private GameObject _Object;
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

        var safeObjectsAmount = 100;
        while(_ObjectsAmount > 0)
        {
            var randomTile = Random.Range(0, totalTiles.Count);
            
            if(totalTiles[randomTile].TileType == _TileType)
            {
                Instantiate(_Object, totalTiles[randomTile].transform.position, Quaternion.identity);
                
                var collectible = _Object.GetComponent<Collectible>();
                if(collectible != null) totalTiles[randomTile].Collectible = collectible;
        
                totalTiles.Remove(totalTiles[randomTile]);
                _ObjectsAmount--;
            }
            else safeObjectsAmount--;

            if(safeObjectsAmount <= 0) break;
        }
    }
}

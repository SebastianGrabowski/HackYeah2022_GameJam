using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGridCreator : MonoBehaviour
{  
    [Space(10)]
    [SerializeField] private int _RowAmount;

    [SerializeField] private float _TileAmount;
    
    [Space(10)]
    [SerializeField] private Vector2 _TileOffset;

    [Space(10)]
    [SerializeField] private WorldTile _Tile;

    public List<WorldTile> WorldTiles { get; private set; } = new List<WorldTile>();

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        var yOffset = 0f;
        var xOffset = 0f;
        for(int j = 0; j < _RowAmount; j++)
        {
            for(int i = 0; i < _TileAmount; i++)
            {
                WorldTile spawnedTile = Instantiate(_Tile, this.transform);

                spawnedTile.transform.position = new Vector2(spawnedTile.transform.position.x + xOffset, spawnedTile.transform.position.y + yOffset);

                WorldTiles.Add(spawnedTile);

                xOffset += _TileOffset.x;
            }

            yOffset += _TileOffset.y;
            xOffset = 0f;
        }
    }
}

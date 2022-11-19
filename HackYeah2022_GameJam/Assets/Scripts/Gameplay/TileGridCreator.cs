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
    [SerializeField] private GameObject _Tile;

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
                var tileObj = Instantiate(_Tile, this.transform);
                tileObj.transform.position = new Vector2(tileObj.transform.position.x + xOffset, tileObj.transform.position.y + yOffset);

                xOffset += _TileOffset.x;
            }

            yOffset += _TileOffset.y;
            xOffset = 0f;
        }
    }
}

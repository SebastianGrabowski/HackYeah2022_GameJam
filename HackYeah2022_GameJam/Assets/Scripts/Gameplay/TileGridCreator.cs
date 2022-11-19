using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGridCreator : MonoBehaviour
{  
    [Space(10)]
    [SerializeField] private int _RowAmount;
    [SerializeField] private float _TileAmount;
    
    [Space(10)]
    [SerializeField] private Vector2 _TilesOffset;

    [Space(10)]
    [SerializeField] private GameObject _Tile;

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        for(int i = 0; i < _TileAmount; i++)
        {
            Instantiate(_Tile, this.transform);
            // _Tile.transform.x += ;
        }
    }
}

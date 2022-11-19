using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererOrder : MonoBehaviour
{

    public int offset;
    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = (int)(-transform.position.y*1000.0f) + offset;
    }
}

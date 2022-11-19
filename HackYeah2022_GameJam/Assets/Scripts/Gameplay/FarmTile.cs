using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTile : MonoBehaviour
{
    private Indicator _Indicator;
    
    private void Awake()
    {
        _Indicator = FindObjectOfType<Indicator>();
    }

    private void OnMouseOver()
    {
        _Indicator.UpdateIndicatorPosition(this.transform.position);
    }
    private void OnMouseExit()
    {
        _Indicator.SetActive(false);
    }
}

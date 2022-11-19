using System.Collections;
using System.Collections.Generic;
using Game.Data;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int Amount;
    public ResourceData ResourceData;

    private bool _CanBeCollected;
    private bool _IsOver;
    
    private void OnMouseOver()
    {
        _IsOver = true;
    }
    
    private void OnMouseExit()
    {
        _IsOver = false;
    }

    public bool CanCollect()
    {
        if(_IsOver && _CanBeCollected) return true;

        return false;
    }
}

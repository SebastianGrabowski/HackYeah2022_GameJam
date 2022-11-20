using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapElement : MonoBehaviour
{
    public Color visibleColor;

    private void Awake()
    {
        GetComponent<UnityEngine.UI.Image>().alphaHitTestMinimumThreshold = 0.3f;
    }

    public void Show()
    {
        GetComponent<UnityEngine.UI.Image>().color = visibleColor;
    }

    public void Hide()
    {
        GetComponent<UnityEngine.UI.Image>().color = Color.clear;
    }
}

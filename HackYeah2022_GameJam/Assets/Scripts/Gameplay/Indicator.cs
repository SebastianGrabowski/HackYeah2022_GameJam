using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _SpriteRenderer;

    public void UpdateIndicatorPosition(Vector2 pos)
    {
        _SpriteRenderer.enabled = true;
        this.transform.position = pos;
    }

    public void SetActive(bool isEnabled)
    {
        _SpriteRenderer.enabled = isEnabled;
    }
}

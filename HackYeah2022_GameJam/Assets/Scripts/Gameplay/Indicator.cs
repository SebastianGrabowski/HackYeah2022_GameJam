using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IndicatorIconType
{
    None,
    Build,
    WoodCollect,
    WoolCollect,
    WheatCollect,
    BuildingDestroy
}

public class Indicator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private SpriteRenderer _IconSpriteRenderer;

    [SerializeField] private Sprite[] _IndicatorIcons;

    public void UpdateIndicatorPosition(Vector2 pos, IndicatorIconType indicatorIconType)
    {
        _SpriteRenderer.enabled = true;
        this.transform.position = pos;

        if(indicatorIconType == IndicatorIconType.None) _IconSpriteRenderer.enabled = false;
        else
        {
            _IconSpriteRenderer.enabled = true;
            _IconSpriteRenderer.sprite = _IndicatorIcons[(int)indicatorIconType - 1];
        }
    }

    public void SetActive(bool isEnabled)
    {
        _SpriteRenderer.enabled = isEnabled;
        _IconSpriteRenderer.enabled = false;
    }
}

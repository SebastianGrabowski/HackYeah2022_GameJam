using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public Image _FillImage;
    public float _TimeToElapse;
    public float _TimeElapsed;

    private bool _IsSet;

    public void SetProgressValue(float time)
    {
        _TimeToElapse = time;
        _TimeElapsed = _TimeToElapse;
        _IsSet = true;
    }

    void Update()
    {
        if(!_IsSet) return;

        _TimeElapsed = _TimeElapsed - Time.deltaTime;
        _FillImage.fillAmount = _TimeElapsed / _TimeToElapse;
    }
}

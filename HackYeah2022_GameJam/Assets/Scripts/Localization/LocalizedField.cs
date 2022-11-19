using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizedField : MonoBehaviour
{

    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = LocalizationController.GetValue(name);
    }
}

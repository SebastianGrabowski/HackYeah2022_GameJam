using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceAmountPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _Amount;
    
    public void SetAmount(int amount)
    {
        if(amount > 0) _Amount.text = "+"+amount;
        else _Amount.text = amount.ToString();

        _Amount.gameObject.SetActive(true);
    }
}

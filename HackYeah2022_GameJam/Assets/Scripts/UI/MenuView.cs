using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuView : MonoBehaviour
{
    
    [SerializeField]private TextMeshProUGUI _TooltipMap;
    [SerializeField]private GameObject _TooltipObj;

    public void RunNewGame(Game.Data.CountryData country)
    {
        Game.Main.PlayerID = country.ID;
        Game.Main.Instance.StartGame();
    }

    public void ExitGame()
    {
        Game.Main.Instance.ExitGame();
    }

    public void SetTooltip(Game.Data.CountryData country)
    {
        _TooltipObj.SetActive(true);
        var t = LocalizationController.GetValue("CountryName_" + country.ID);
        _TooltipMap.text = "<color=orange>Click here to start game with selected country:</color>"+t;
    }

    public void HideTooltip()
    {
        _TooltipObj.SetActive(false);
    }
}

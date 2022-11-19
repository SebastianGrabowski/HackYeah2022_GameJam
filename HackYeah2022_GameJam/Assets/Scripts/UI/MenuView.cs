using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : MonoBehaviour
{

    public void RunNewGame()
    {
        Game.Main.Instance.StartGame();
    }

    public void ExitGame()
    {
        Game.Main.Instance.ExitGame();
    }
}

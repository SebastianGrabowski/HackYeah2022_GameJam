namespace Game
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameplayView : MonoBehaviour
    {


        public void BreakGame()
        {
            Main.Instance.BreakGame();
        }
    }
}
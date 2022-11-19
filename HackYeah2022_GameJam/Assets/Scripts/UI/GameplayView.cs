namespace Game
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;

    public class GameplayView : MonoBehaviour
    {
        [SerializeField]private TextMeshProUGUI _MoneyLabel;
        [SerializeField]private TextMeshProUGUI _PeopleLabel;

        public void BreakGame()
        {
            Main.Instance.BreakGame();
        }


        private void Update()
        {
            var gc = Gameplay.GameplayController.Instance;
            _MoneyLabel.text = gc.Money.ToString();
            _PeopleLabel.text = gc.People[4].ToString();
        }
    }
}
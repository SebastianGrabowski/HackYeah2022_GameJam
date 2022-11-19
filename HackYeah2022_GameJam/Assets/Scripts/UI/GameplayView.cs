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
        [SerializeField]private BuildWindow _BuildWindow;

        public static GameplayView Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void BreakGame()
        {
            Main.Instance.BreakGame();
        }
        public void OpenSettings()
        {
            Main.Instance.BreakGame();
        }

        public void OpenBuildWindow(WorldTile tile)
        {
            _BuildWindow.Open(tile);
        }

        private void Update()
        {
            var gc = Gameplay.GameplayController.Instance;
            _MoneyLabel.text = gc.Money.ToString();
            _PeopleLabel.text = gc.People[4].ToString();
        }
    }
}
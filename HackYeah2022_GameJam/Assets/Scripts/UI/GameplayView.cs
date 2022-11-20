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

        [Header("Destroy")]
        [SerializeField]private UnityEngine.UI.Image _DestroyIcon;
        [SerializeField]private GameObject _DestroyLabel;

        public static GameplayView Instance;

        private void Awake()
        {
            Instance = this;
            _DestroyIcon.color = Color.gray;
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
        }

        public void DestroyClickHandler()
        {
            Gameplay.GameplayController.Instance.DestroyBuildingMode = !Gameplay.GameplayController.Instance.DestroyBuildingMode;
            _DestroyIcon.color = Gameplay.GameplayController.Instance.DestroyBuildingMode ? Color.white : Color.gray;
            _DestroyLabel.SetActive(Gameplay.GameplayController.Instance.DestroyBuildingMode);
        }
    }
}
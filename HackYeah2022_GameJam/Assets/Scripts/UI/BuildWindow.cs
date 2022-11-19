namespace Game
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;

    public class BuildWindow : MonoBehaviour
    {

        [SerializeField]private GameObject _View;
        [SerializeField]private UnityEngine.UI.Image _Blocker;
        [SerializeField]private GameObject _Template;
        [SerializeField]private TextMeshProUGUI _Desc;

        private List<UnityEngine.UI.Button> _Buttons = new List<UnityEngine.UI.Button>();

        private WorldTile _Tile;

        private void Start()
        {
            var c = Data.DataController.Instance.BuildingData.Length;
            for(var i = 0; i < c; i++)
            {
                var b = Data.DataController.Instance.GetBuildingData(i);
                var newItem = Instantiate(_Template, _Template.transform.parent);
                newItem.gameObject.SetActive(true);

                var button = newItem.GetComponent<UnityEngine.UI.Button>();
                _Buttons.Add(button);
                var e = newItem.GetComponent<UnityEngine.EventSystems.EventTrigger>();

                UnityEngine.EventSystems.EventTrigger.Entry a1 = new UnityEngine.EventSystems.EventTrigger.Entry();
                a1.eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter;
                a1.callback.AddListener(
                    (c) =>
                    {
                        _Desc.text = LocalizationController.GetValue("BuildingDesc_" + b.ID.ToString()).ToLower();
                    }
                );
                
                UnityEngine.EventSystems.EventTrigger.Entry a2 = new UnityEngine.EventSystems.EventTrigger.Entry();
                a2.eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit;
                a2.callback.AddListener(
                    (c) =>
                    {
                        _Desc.text = string.Empty;
                    }
                );

                e.triggers.Add(a1);
                e.triggers.Add(a2);

                button.interactable = Random.Range(0, 100) < 50;

            }
        }

        public void Open(WorldTile tile)
        {
            _Tile = tile;
            _Blocker.enabled = true;
            _View.SetActive(true);

            var gameplayController = Game.Gameplay.GameplayController.Instance;

            var i = 0;
            foreach(var b in _Buttons)
            {
                var data = Data.DataController.Instance.GetBuildingData(i);
                b.interactable = gameplayController.CanBuild(data);
                var j = i;
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(()=>{ 
                    var k = j;
                    _Tile.Build(k);
                    Close();
                    gameplayController.BuildHandler(data);
                });
                i++;
            }
        }

        public void Close()
        {
            _Blocker.enabled = false;
            _View.SetActive(false);
        }
    }
}
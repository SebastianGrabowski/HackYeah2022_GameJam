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
        [SerializeField]private TextMeshProUGUI _Name;
        [SerializeField]private TextMeshProUGUI _Desc;
        [SerializeField]private TextMeshProUGUI _Req;
        [SerializeField]private TextMeshProUGUI[] _ReqResLabels;
        [SerializeField]private GameObject _ReqLabelHeader;

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
                        _Name.text = LocalizationController.GetValue("BuildingName_" + b.ID.ToString()).ToLower();
                        _Req.text = GetReqText(b);
                        var any = SetReqRes(b);
                        if (any || !string.IsNullOrEmpty(_Req.text))
                            _ReqLabelHeader.SetActive(true);
                    }
                );
                
                UnityEngine.EventSystems.EventTrigger.Entry a2 = new UnityEngine.EventSystems.EventTrigger.Entry();
                a2.eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit;
                a2.callback.AddListener(
                    (c) =>
                    {
                        _ReqLabelHeader.SetActive(false);
                        _Desc.text = string.Empty;
                        _Name.text = string.Empty;
                        _Req.text = string.Empty;
                        for(var i = 0; i < _ReqResLabels.Length; i++)
                        {
                            _ReqResLabels[i].transform.parent.gameObject.SetActive(false);
                        }
                    }
                );

                e.triggers.Add(a1);
                e.triggers.Add(a2);

                button.interactable = Random.Range(0, 100) < 50;

            }
        }

        private string GetReqText(Data.BuildingData data)
        {
            var result = string.Empty;
            if(data.ReqBuildings != null)
            {
                for(var i = 0; i < data.ReqBuildings.Length; i++)
                {
                    var b = data.ReqBuildings;
                    var has = Game.Gameplay.GameplayController.Instance.HasBuilding(b[i].ID);
                    var bName = LocalizationController.GetValue("BuildingName_" + b[i].ID.ToString()).ToUpper();
                    if (has)
                    {
                        result += "<color=green>" + bName + "</color>" + (i < (data.ReqBuildings.Length-1) ? ", " : "");
                    } else
                    {
                        result += "<color=red>" + bName + "</color>" + (i < (data.ReqBuildings.Length-1) ? ", " : "");
                    }
                }
            }
            return result;
        }

        private bool SetReqRes(Data.BuildingData data)
        {
            var result = false;
            for(var i = 0; i < _ReqResLabels.Length; i++)
            {
                _ReqResLabels[i].transform.parent.gameObject.SetActive(false);
            }
            if(data.BuildCost != null)
            {
                for(var i = 0; i < data.BuildCost.Length; i++)
                {
                    result = true;
                    var res = data.BuildCost[i].Resource.ID;
                    _ReqResLabels[res].transform.parent.gameObject.SetActive(true);
                    _ReqResLabels[res].transform.parent.gameObject.SetActive(true);
                    _ReqResLabels[res].text = data.BuildCost[i].Value.ToString();
                    _ReqResLabels[res].color = (Gameplay.GameplayController.Instance.Resources[res] >= data.BuildCost[i].Value) ? Color.green : Color.red;
                }
            }
            return result;
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
namespace Game.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class Building : MonoBehaviour
    {
        
        [SerializeField]private ProgressBar _ProgressBar;

        public int ActiveBuildingID = -1;
        public bool Build;
        
        [SerializeField]private GameObject[] _Renderers;

        public bool ProcessReady;
        public bool ProcessCheckIn = false;
        public float ProcessTime;

        public Data.BuildingData _Data;
        
        [SerializeField] private GameObject _NotificationAlert;
        [SerializeField] private Vector2 _NotificationOffset;
        private GameObject _NotificationObj;

        private ProgressBar _ProcessProgress;

        public void Set(int buildingDataID)
        {
            ActiveBuildingID = buildingDataID;
            
            for(var i = 0; i < _Renderers.Length; i++)
            {
                _Renderers[i].SetActive(false);
            }

            if(ActiveBuildingID != -1)
            {
                _Data = Data.DataController.Instance.GetBuildingData(ActiveBuildingID);
                var spawnPos = new Vector2(transform.position.x, transform.position.y);
                var progressBar = Instantiate(_ProgressBar, spawnPos, Quaternion.identity);
                progressBar.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                
                var data = Data.DataController.Instance.GetBuildingData(ActiveBuildingID);
                progressBar.SetProgressValue(data.BuildTime);

                Destroy(progressBar.gameObject, data.BuildTime);

                //_Renderer.enabled = true;
                //_Renderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                //_Renderer.sprite = data.ViewSprite;

                Invoke("BuildDone", data.BuildTime);
            } else
            {
                _Data = null;
            }
        }

        private void BuildDone()
        {
            _Renderers[ActiveBuildingID].SetActive(true);
            Build = true;
            var data = Data.DataController.Instance.GetBuildingData(ActiveBuildingID);
            Gameplay.GameplayController.Instance.BuildEndHandler(data);
            ProcessCheckIn = true;
            //_Renderer.color = Color.white;
        }

        public void DestroyHandler()
        {
            if (_ProcessProgress != null && _ProcessProgress.gameObject != null)
            {
                Destroy(_ProcessProgress.gameObject);
            }
            if(_NotificationObj != null) Destroy(_NotificationObj);
        }

        private void Update()
        {
            if(Build && _Data != null && _Data.ProcessSell)
            {
                if (ProcessCheckIn)
                {
                    if (TryRunProcess())
                    {
                        ProcessReady = false;
                        ProcessCheckIn = false;
                        ProcessTime = 0;
                        var spawnPos = new Vector2(transform.position.x, transform.position.y);
                        _ProcessProgress = Instantiate(_ProgressBar, spawnPos, Quaternion.identity);
                        _ProcessProgress.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        _ProcessProgress.SetProgressValue(_Data.ProcessTime);
                        Destroy(_ProcessProgress.gameObject, _Data.ProcessTime);

                    }
                } else if (!ProcessReady)
                {
                    ProcessTime += Time.deltaTime;
                    if(ProcessTime >= _Data.ProcessTime)
                    {
                        ProcessReady = true;
                        
                        if(_NotificationObj != null) Destroy(_NotificationObj);
                        var spawnPos = new Vector2(transform.position.x + _NotificationOffset.x, transform.position.y + _NotificationOffset.y);
                        _NotificationObj = Instantiate(_NotificationAlert, spawnPos, Quaternion.identity);
                        _NotificationObj.transform.localScale = new Vector3(4f, 4f, 4f);
                    }
                }
            }
            
        }

        public bool TryRunProcess()
        {
            var gc = Gameplay.GameplayController.Instance;
            if(_Data.ProcessCost != null)
            {
                for(var i = 0; i < _Data.ProcessCost.Length; i++)
                {
                    var res = _Data.ProcessCost[i].Resource.ID;
                    var value = _Data.ProcessCost[i].Value;
                    if (gc.Resources[res] < value)
                    {
                        return false;
                    }
                }
                
                for(var i = 0; i < _Data.ProcessCost.Length; i++)
                {
                    var res = _Data.ProcessCost[i].Resource.ID;
                    var value = _Data.ProcessCost[i].Value;
                    gc.ChangeResource(res, -value);
                }
            }
            return true;
        }
        public void AddProcessAction()
        {
            ProcessCheckIn = true;
            ProcessReady = false;
            //add resources here
            var gc = Gameplay.GameplayController.Instance;
            gc.Resources[5] += _Data.ProcessMoney;
            if(_NotificationObj != null) Destroy(_NotificationObj);
            Main.Instance.CoinSFX.Play();
        }
    }
}
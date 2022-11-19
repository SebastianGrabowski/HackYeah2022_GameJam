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

        public void Set(int buildingDataID)
        {
            ActiveBuildingID = buildingDataID;
            
            for(var i = 0; i < _Renderers.Length; i++)
            {
                _Renderers[i].SetActive(false);
            }

            if(ActiveBuildingID != -1)
            {

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
            }
        }

        private void BuildDone()
        {
            _Renderers[ActiveBuildingID].SetActive(true);
            Build = true;
            //_Renderer.color = Color.white;
        }
    }
}
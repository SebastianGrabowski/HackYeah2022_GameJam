namespace Game.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class Building : MonoBehaviour
    {
        
        [SerializeField] private ProgressBar _ProgressBar;

        public int ActiveBuildingID = -1;
        public bool Build;
        
        public SpriteRenderer _Renderer;

        public void Set(int buildingDataID)
        {
            ActiveBuildingID = buildingDataID;
            if(ActiveBuildingID == -1)
            {
                _Renderer.enabled = false;
            } else
            {
                var spawnPos = new Vector2(transform.position.x, transform.position.y + 0.5f);
                var progressBar = Instantiate(_ProgressBar, spawnPos, Quaternion.identity);
                progressBar.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                
                var data = Data.DataController.Instance.GetBuildingData(ActiveBuildingID);
                progressBar.SetProgressValue(data.BuildTime);

                Destroy(progressBar.gameObject, data.BuildTime);

                _Renderer.enabled = true;
                _Renderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                _Renderer.sprite = data.ViewSprite;

                Invoke("BuildDone", data.BuildTime);
            }
        }

        private void BuildDone()
        {
            Build = true;
            _Renderer.color = Color.white;
        }
    }
}
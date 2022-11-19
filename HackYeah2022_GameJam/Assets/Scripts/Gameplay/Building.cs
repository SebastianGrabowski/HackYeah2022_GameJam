namespace Game.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class Building : MonoBehaviour
    {

        public int ActiveBuildingID = -1;

        public SpriteRenderer _Renderer;

        public void Set(int buildingDataID)
        {
            ActiveBuildingID = buildingDataID;
            if(ActiveBuildingID == -1)
            {
                _Renderer.enabled = false;
            } else
            {
                _Renderer.enabled = true;
                var data = Data.DataController.Instance.GetBuildingData(ActiveBuildingID);
                _Renderer.sprite = data.ViewSprite;
            }
        }
    }
}
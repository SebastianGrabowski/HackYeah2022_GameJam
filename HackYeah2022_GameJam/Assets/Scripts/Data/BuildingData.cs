namespace Game.Data
{
    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "BuildingData", menuName = "GameData/BuildingData", order = 1)]
    public class BuildingData : ScriptableObject
    {

        [System.Serializable]
        public class ResourceValue
        {
            public ResourceData Resource;
            public int Value;
        }
        
        public int ID;
        public ResourceValue[] BuildCost;
        public BuildingData[] ReqBuildings;
        public float BuildTime;
        public Sprite ViewSprite;
        public Sprite Icon;
        public int ChangePeople;
    }
}
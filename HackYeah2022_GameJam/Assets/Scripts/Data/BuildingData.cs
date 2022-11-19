namespace Game.Data
{
    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "BuildingData", menuName = "GameData/BuildingData", order = 1)]
    public class BuildingData : ScriptableObject
    {

        public class ResourceValue
        {
            public ResourceData Resource;
            public int Value;
        }

        public ResourceValue[] BuildCost;
        public float BuildTime;
    }
}
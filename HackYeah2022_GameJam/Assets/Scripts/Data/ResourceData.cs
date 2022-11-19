namespace Game.Data
{
    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "ResourceData", menuName = "GameData/ResourceData", order = 1)]
    public class ResourceData: ScriptableObject
    {
        
        public int ID;

        public Sprite Icon;

    }
}
namespace Game.Data
{
    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "CountryData", menuName = "GameData/CountryData", order = 1)]
    public class CountryData : ScriptableObject
    {

        public int ID;

        public Sprite Flag;


    }
}
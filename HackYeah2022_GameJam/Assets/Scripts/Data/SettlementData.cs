namespace Game.Data
{
    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "SettlementData", menuName = "GameData/SettlementData", order = 1)]
    public class SettlementData : ScriptableObject
    {
        
        public int ID;

        public CountryData Country;

        public Vector2 MapPosition;

        public int StartMoney;
        public int[] StartResources;
        public int StartPeople;

    }
}
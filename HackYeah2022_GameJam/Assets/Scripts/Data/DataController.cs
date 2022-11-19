namespace Game.Data
{
    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DataController : MonoSingleton<DataController>
    {

        [HideInInspector]public CountryData[] CountryData;
        [HideInInspector]public SettlementData[] SettlementData;
        [HideInInspector]public ResourceData[] ResourceData;
        [HideInInspector]public BuildingData[] BuildingData;

        public const string CountryDataDir = "CountryData";
        public const string SettlementDataDir = "SettlementData";
        public const string ResourceDataDir = "ResourceData";
        public const string BuildingDataDir = "BuildingData";

        protected override void OnAwake()
        {
            base.OnAwake();

            CountryData = Resources.LoadAll<CountryData>(CountryDataDir);
            SettlementData = Resources.LoadAll<SettlementData>(SettlementDataDir);
            ResourceData = Resources.LoadAll<ResourceData>(ResourceDataDir);
            BuildingData = Resources.LoadAll<BuildingData>(BuildingDataDir);
        }

        public CountryData GetCountryData(int id)
        {
            for(var i = 0; i < CountryData.Length; i++)
            {
                if (CountryData[i].ID == id)
                {
                    return CountryData[i];
                }
            }
            return null;
        }

        public SettlementData GetSettlementData(int id)
        {
            for(var i = 0; i < SettlementData.Length; i++)
            {
                if (SettlementData[i].ID == id)
                {
                    return SettlementData[i];
                }
            }
            return null;
        }

        public ResourceData GetResourceData(int id)
        {
            for(var i = 0; i < ResourceData.Length; i++)
            {
                if (ResourceData[i].ID == id)
                {
                    return ResourceData[i];
                }
            }
            return null;
        }

        public BuildingData GetBuildingData(int id)
        {
            for(var i = 0; i < BuildingData.Length; i++)
            {
                if (BuildingData[i].ID == id)
                {
                    return BuildingData[i];
                }
            }
            return null;
        }
    }
}
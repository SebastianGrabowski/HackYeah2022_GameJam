namespace Game.Gameplay
{
    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameplayController : MonoSingleton<GameplayController>
    {
        [HideInInspector]public int Money;
        [HideInInspector]public int[] Resources;
        [HideInInspector]public int[] People;

        private float _T;

        protected override void OnAwake()
        {
            Resources = new int[Data.DataController.Instance.ResourceData.Length];
            People = new int[Data.DataController.Instance.SettlementData.Length];
            var settlement = Data.DataController.Instance.GetSettlementData(4); //4=PL
            Resources = settlement.StartResources;
            Money = settlement.StartMoney;
            People[4] = settlement.StartPeople;
        }

        private void Update()
        {
            _T += Time.deltaTime;
            if(_T > 0.4f)
            {
                _T = 0.0f;
               
                var r = Random.Range(0, Resources.Length);
                People[r] = Mathf.Clamp(People[r] + Random.Range(-1, 3), 0, 1000);  
            }
        }

        public bool CanBuild(Data.BuildingData building)
        {
            var allBuildings = FindObjectsOfType<Building>();
            if(building.ReqBuildings != null)
            {
                for(var i = 0; i < building.ReqBuildings.Length; i++)
                {
                    var hasBuilding = false;
                    for(var j = 0; j < allBuildings.Length; j++)
                    {
                        if (allBuildings[j].Build && allBuildings[j].ActiveBuildingID == building.ReqBuildings[i].ID)
                        {
                            hasBuilding = true;
                            break;
                        }
                    }
                    if (!hasBuilding)
                    {
                        return false;
                    }
                }
            }
            if (building.BuildCost != null)
            {
                for(var i = 0; i < building.BuildCost.Length; i++)
                {
                    if(Resources[(int)building.BuildCost[i].Resource.ID] < building.BuildCost[i].Value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void BuildHandler(Data.BuildingData building)
        {
            if (building.BuildCost != null)
            {
                for(var i = 0; i < building.BuildCost.Length; i++)
                {
                    Resources[(int)building.BuildCost[i].Resource.ID] -= building.BuildCost[i].Value;
                }
            }
        }

    }
}

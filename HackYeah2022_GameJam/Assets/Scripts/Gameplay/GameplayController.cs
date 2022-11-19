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
                Resources[Random.Range(0, Resources.Length)]++;

                var r = Random.Range(0, Resources.Length);
                People[r] = Mathf.Clamp(People[r] + Random.Range(-1, 3), 0, 1000);  
            }
        }

    }
}

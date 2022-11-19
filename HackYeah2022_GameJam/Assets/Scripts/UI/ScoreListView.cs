namespace Game
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using System.Linq;

    public class ScoreListView : MonoBehaviour
    {
        [SerializeField]private GameObject _TemplateItem;

        private List<GameObject> _SpawnedItems = new List<GameObject>();

        private float _T;

        // Start is called before the first frame update
        void Start()
        {
            Refresh();
        }

        // Update is called once per frame
        void Update()
        {
            _T += Time.deltaTime;
            if(_T > 1.0f)
            {
                _T = 0.0f;
                Refresh();
            }
        }

        private void Refresh()
        {
            var settlementsData = Data.DataController.Instance.SettlementData;
            
            for(var i = 0; i < _SpawnedItems.Count; i++)
            {
                Destroy(_SpawnedItems[i].gameObject);
            }
            _SpawnedItems.Clear();

            List<KeyValuePair<int, int>> a = new List<KeyValuePair<int, int>>();
            
            var people = Game.Gameplay.GameplayController.Instance.People;
            foreach(var s in settlementsData)
            {
                a.Add(new KeyValuePair<int, int>(s.ID, people[s.ID]));
            }

            a = a.OrderByDescending(x=>x.Value).ToList();

            foreach(var s in a)
            {
                var newItem = Instantiate(_TemplateItem, _TemplateItem.transform.parent);
                newItem.gameObject.SetActive(true);
                var settlement = Data.DataController.Instance.GetSettlementData(s.Key);
                newItem.transform.GetChild(0).GetComponent<Image>().sprite = settlement.Country.Flag;
                newItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = LocalizationController.GetValue("SettlementName_" + s.Key.ToString());
                newItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = s.Value.ToString();
                _SpawnedItems.Add(newItem);
            }
        }
    }
}
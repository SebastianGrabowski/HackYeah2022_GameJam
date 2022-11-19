namespace Game
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

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

            foreach(var s in settlementsData)
            {
                var newItem = Instantiate(_TemplateItem, _TemplateItem.transform.parent);
                newItem.gameObject.SetActive(true);
                newItem.transform.GetChild(0).GetComponent<Image>().sprite = s.Country.Flag;
                newItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = LocalizationController.GetValue("SettlementName_" + s.ID.ToString());
                newItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "+22";
                _SpawnedItems.Add(newItem);
            }
        }
    }
}
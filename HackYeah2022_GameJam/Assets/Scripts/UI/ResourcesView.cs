namespace Game
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class ResourcesView : MonoBehaviour
    {

        [SerializeField]private GameObject _Template;

        private List<TextMeshProUGUI> _ValueLabels = new List<TextMeshProUGUI>();

        private bool _Initialized;

        void Start()
        {
            var c = Data.DataController.Instance.ResourceData.Length;
            _Template.gameObject.SetActive(true);
            for(var i = 0; i < c; i++)
            {
                var res = Data.DataController.Instance.GetResourceData(i);
                var newItem = Instantiate(_Template, _Template.transform.parent);
                var valueLabel = newItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                newItem.transform.GetChild(1).GetComponent<Image>().sprite = res.Icon;
                newItem.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = "TOOLTIP " + i.ToString();
                _ValueLabels.Add(valueLabel);
            }
            _Template.gameObject.SetActive(false);

            _Initialized = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (_Initialized)
            {
                for(var i = 0; i < _ValueLabels.Count; i++)
                {
                    _ValueLabels[i].text = i.ToString();
                }
            }
        }
    }
}
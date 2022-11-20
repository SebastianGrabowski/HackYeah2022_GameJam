using System.Collections;
using System.Collections.Generic;
using Game.Data;
using UnityEngine;

public enum CollectibleType
{
    Wood,
    Wool,
    Wheat
}

public class Collectible : MonoBehaviour
{
    
    [SerializeField] private float _CollectionTime;

    [SerializeField] private Vector2 _CollectionProgressOffset;
    [SerializeField] private Vector2 _NotificationOffset;

    [SerializeField] private Animator _Animator;
    [SerializeField] private SpriteRenderer _SpriteRenderer;

    [SerializeField] private Sprite _SpriteCollected;
    [SerializeField] private Sprite[] _WheatSprites;

    [Space(10)]

    [SerializeField] private int[] _Amount;
    [SerializeField] private ResourceData[] _ResourceData;

    [Space(10)]
    
    [SerializeField] private ProgressBar _ProgressBar;

    [SerializeField] private GameObject _NotificationAlert;

    public CollectibleType CollectibleType;

    private WorldGenerator _WorldGenerator;
    private GameObject _NotificationObj;
    private Sprite _StartSprite;

    private bool _CanBeCollected;
    private bool _IsCollected;
    private bool _Once = false;
    private bool _IsOver;

    private float _TimeToElapse = 0f;
    private float _CurrentTime = 0f;
    

    void Awake()
    {
        _WorldGenerator = FindObjectOfType<WorldGenerator>();

        if (_SpriteRenderer != null)
            _StartSprite = _SpriteRenderer.sprite;
    }

    void Start()
    {
        if(CollectibleType == CollectibleType.Wheat) 
        {
            _TimeToElapse = Time.time + _CollectionTime;
            _IsCollected = true;

            var spawnPos = new Vector2(transform.position.x + _CollectionProgressOffset.x, transform.position.y + _CollectionProgressOffset.y);
            var progressBar = Instantiate(_ProgressBar, spawnPos, Quaternion.identity);
            progressBar.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            _TimeToElapse = Time.time + _CollectionTime;
            if(CollectibleType == CollectibleType.Wheat) progressBar.SetProgressValue(_CollectionTime);

            Destroy(progressBar.gameObject, (_CollectionTime - 0.05f));
        }
    }

    private void OnMouseOver()
    {
        _IsOver = true;
    }
    
    private void OnMouseExit()
    {
        _IsOver = false;
    }

    public bool CanCollect()
    {
        // if(_IsOver && _CanBeCollected) return true;
        return true;
    }

    void Update()
    {
        if(CollectibleType == CollectibleType.Wood)
        {
            if(_IsCollected && Time.time >= _TimeToElapse)
            {
                _WorldGenerator.OnTreeCollected();
                _Animator.SetTrigger("Collect");
                Invoke(nameof(Collected), 1f);
            }
        }
        else if(CollectibleType == CollectibleType.Wool)
        {
            if(_IsCollected && Time.time >= _TimeToElapse)
            {
                RespawnWool();
            }
        }
        else if(CollectibleType == CollectibleType.Wheat)
        {
            _CurrentTime += Time.deltaTime;
            if(_CurrentTime > _CollectionTime) _CurrentTime = _CollectionTime;
            var time = _CurrentTime / _CollectionTime;
            Debug.Log("CurrentTime: "+time);
            if(time > 0.33f && time <= 0.66f) 
            {
                _SpriteRenderer.sprite = _WheatSprites[1];
                _Animator.enabled = false;
            }

            if(_IsCollected && Time.time >= _TimeToElapse)
            {
                RespawnWheat();
            }
        }
           
    }

    private void RespawnWool()
    {
        var spawnPos = new Vector2(transform.position.x + _NotificationOffset.x, transform.position.y + _NotificationOffset.y);
        _NotificationObj = Instantiate(_NotificationAlert, spawnPos, Quaternion.identity);
        _NotificationObj.transform.localScale = new Vector3(4f, 4f, 4f);
        _SpriteRenderer.sprite = _StartSprite;
        _IsCollected = false;
    }

    private void RespawnWheat()
    {
        var spawnPos = new Vector2(transform.position.x + _NotificationOffset.x, transform.position.y + _NotificationOffset.y);
        _NotificationObj = Instantiate(_NotificationAlert, spawnPos, Quaternion.identity);
        _NotificationObj.transform.localScale = new Vector3(4f, 4f, 4f);
        _SpriteRenderer.sprite = _WheatSprites[2];
        _IsCollected = false;
        _Animator.enabled = false;
    }

    public void Collect()
    {
        if(_IsCollected) return;
        
        var spawnPos = new Vector2(transform.position.x + _CollectionProgressOffset.x, transform.position.y + _CollectionProgressOffset.y);
        var progressBar = Instantiate(_ProgressBar, spawnPos, Quaternion.identity);
        progressBar.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        _TimeToElapse = Time.time + _CollectionTime;
        progressBar.SetProgressValue(_CollectionTime);

        Destroy(progressBar.gameObject, (_CollectionTime - 0.05f));

        if(CollectibleType == CollectibleType.Wool) 
        {
            if(_NotificationObj != null) Destroy(_NotificationObj);
            _Animator.SetTrigger("WoolCollect");
            _SpriteRenderer.sprite = _SpriteCollected;

            for(int i = 0; i < _ResourceData.Length; i++)
            {
                Game.Gameplay.GameplayController.Instance.ChangeResource(_ResourceData[i].ID, _Amount[i]);
            }
            
        }
        else if(CollectibleType == CollectibleType.Wheat) 
        {
            if(_NotificationObj != null) Destroy(_NotificationObj);
            _Animator.enabled = true;
            _Animator.SetTrigger("WoolCollect");
            _SpriteRenderer.sprite = _WheatSprites[0];

            for(int i = 0; i < _ResourceData.Length; i++)
            {
                Game.Gameplay.GameplayController.Instance.ChangeResource(_ResourceData[i].ID, _Amount[i]);
            }
            
        }

        _IsCollected = true;
    }

    void Collected()
    {
        if(_Once) return;

        for(int i = 0; i < _ResourceData.Length; i++)
        {
            Game.Gameplay.GameplayController.Instance.ChangeResource(_ResourceData[i].ID, _Amount[i]);
        }
        
        Destroy(this.gameObject);
        _Once = true;
    }
}

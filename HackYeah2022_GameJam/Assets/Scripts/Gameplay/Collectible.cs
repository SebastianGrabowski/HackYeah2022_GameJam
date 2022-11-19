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
    [SerializeField] private int _Amount;
    [SerializeField] private float _CollectionTime;
    [SerializeField] private Vector2 _CollectionProgressOffset;

    [SerializeField] private Animator _Animator;
    [SerializeField] private Sprite _SpriteCollected;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private ResourceData _ResourceData;
    [SerializeField] private ProgressBar _ProgressBar;

    [SerializeField] private CollectibleType _CollectibleType;

    private Sprite _StartSprite;

    private bool _CanBeCollected;
    private bool _IsCollected; 
    private bool _IsOver;

    private float _TimeToElapse = 0;
    

    void Awake()
    {
        _StartSprite = _SpriteRenderer.sprite;
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
        if(_CollectibleType == CollectibleType.Wood)
        {
            if(_IsCollected && Time.time >= _TimeToElapse)
            {
                _Animator.SetTrigger("Collect");
                Invoke(nameof(Collected), 1f);
            }
        }
        else if(_CollectibleType == CollectibleType.Wool)
        {
            if(_IsCollected && Time.time >= _TimeToElapse)
            {
                RespawnWool();
            }
        }
           
    }

    private void RespawnWool()
    {
        _SpriteRenderer.sprite = _StartSprite;
        _IsCollected = false;
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

        if(_CollectibleType == CollectibleType.Wool) 
        {
            _SpriteRenderer.sprite = _SpriteCollected;
            Game.Gameplay.GameplayController.Instance.Resources[_ResourceData.ID] += _Amount;
        }

        _IsCollected = true;
    }

    void Collected()
    {
        Game.Gameplay.GameplayController.Instance.Resources[_ResourceData.ID] += _Amount;
        Destroy(this.gameObject);
    }
}

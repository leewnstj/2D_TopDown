using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : PoolableMono
{
    [SerializeField] private ResourceDataSO _itemData;
    private AudioSource _audioSource;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _itemData.UseSound;
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PickUpResource()
    {
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        _collider.enabled = false;
        _spriteRenderer.enabled = false;
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length + 0.3f);
        PoolManager.Instance.Push(this);
    }



    public override void Reset()
    {
        gameObject.layer = LayerMask.NameToLayer("Item");
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
    }
}

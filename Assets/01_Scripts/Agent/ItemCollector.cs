using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private float _magneticRange = 2f, _magneticPower = 2f;
    [SerializeField] private LayerMask _whatIsItem;

    private List<ItemScript> _collectList = new ();

    private void FixedUpdate()
    {
        Collider2D[] resources = Physics2D.OverlapCircleAll(transform.position, _magneticRange, _whatIsItem);
        
        foreach(Collider2D r in resources)
        {
            if( r.TryGetComponent<ItemScript>(out ItemScript item))
            {
                _collectList.Add(item);
                item.gameObject.layer = 0;
            }
        }

        for(int i = 0; i < _collectList.Count; i++)
        {
            ItemScript item = _collectList[i];
            Vector2 dir = (transform.position - item.transform.position).normalized;
            item.transform.Translate(dir * _magneticPower * Time.deltaTime);

            if(Vector2.Distance(transform.position, item.transform.position) < 0.1f)
            {
                item.PickUpResource(); //아이템 줍기
                _collectList.RemoveAt(i);
                i--;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _magneticRange);
            Gizmos.color = Color.white;
        }
    }
}

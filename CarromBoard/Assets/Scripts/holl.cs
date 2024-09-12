using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holl : MonoBehaviour
{
   public ColliderCaracm _ColliderCaracm;
    public CoinCollect cc;
    private void Start()
    {
        _ColliderCaracm = GetComponentInParent<ColliderCaracm>();
        cc=GetComponentInParent<CoinCollect>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enetr");
        if (collision.gameObject.CompareTag("coin"))
        {

        }
        else if(collision.gameObject.CompareTag("Red"))
        {

          
        }
     
    }
}

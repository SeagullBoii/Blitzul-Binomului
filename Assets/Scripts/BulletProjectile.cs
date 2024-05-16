using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] public float damage;
    
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}

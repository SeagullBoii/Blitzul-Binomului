using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField]public float bulletSpeed;
    [SerializeField] Rigidbody bulletRigidbody;

    private void Update()
    {
        bulletRigidbody.linearVelocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}

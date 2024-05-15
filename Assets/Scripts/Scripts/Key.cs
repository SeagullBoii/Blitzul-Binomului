using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] [Tooltip("0=Blue, \n 1=Yellow, \n 2=Red.")] int index;
    private void OnTriggerEnter(Collider other) { 
        if (other.TryGetComponent<Inventory>(out Inventory inv))
        {
            inv.UnlockKey(index);
            gameObject.SetActive(false);
        }
    }
}

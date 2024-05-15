using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class DoorOpener : MonoBehaviour
{
    [SerializeField] float range = 0.3f;
    [SerializeField] LayerMask doorLayer;

    Inventory inv;

    private void Start()
    {
        inv = GetComponent<Inventory>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            OpenDoor();
        }
    }

    /// <summary>
    /// Tragem o linie invizibila de la jucator, in fata. 
    /// Daca gasim o usa, o deschidem.
    /// </summary>
    private void OpenDoor()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, range, doorLayer))
        {
            if (hitInfo.collider.gameObject.TryGetComponent<Door>(out Door door))
                if (!door.GetOpen())
                     door.Open(inv);

        }
    }
}


using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] Inventory inv;
    [SerializeField] GameObject blueKey;
    [SerializeField] GameObject yellowKey;
    [SerializeField] GameObject redKey;


    private void Update()
    {
        HandleKeys();
    }

    private void HandleKeys()
    {
        if (inv.IsKeyUnlocked(0))
            blueKey.SetActive(true);
       
        if (inv.IsKeyUnlocked(1))
            yellowKey.SetActive(true);

        if (inv.IsKeyUnlocked(2))
            redKey.SetActive(true);

    }
}

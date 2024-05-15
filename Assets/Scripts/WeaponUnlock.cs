using UnityEngine;

public class WeaponUnlock : MonoBehaviour
{

[SerializeField] int index;
[SerializeField] bool permanent = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<WeaponUnlocker>(out WeaponUnlocker weaponUnlocker))
        {
            SaveData.unlockedWeapons[index] = true;
            weaponUnlocker.handler.Equip(index);
            if (permanent)
            {
                switch (index)
                {
                    case 0:
                        PlayerPrefs.SetInt("HasSword", 1);
                        break;
                    case 1:
                        PlayerPrefs.SetInt("HasProtractor", 1);
                        break;
                    case 2:
                        PlayerPrefs.SetInt("HasSineCosine", 1);
                        break;
                    case 3:
                        PlayerPrefs.SetInt("HasBoomerang", 1);
                        break;
                }
                PlayerPrefs.Save();
            }
            gameObject.SetActive(false);
        }
    }
}

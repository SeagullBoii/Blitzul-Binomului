using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] Weapon[] weapons;
    [SerializeField] Animator weaponAnimator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [Header("Projectiles")]
    [SerializeField] GameObject protractorProjectile;

    int firstUnlockedWeapon = -1;
    int currentWeaponIndex;
    float attackCD;
    private void Start()
    {
        SaveData.SetGunUnlocks();
        for (int i = 0; i < SaveData.unlockedWeapons.Length; i++)
            if (SaveData.unlockedWeapons[i])
                firstUnlockedWeapon = i;




        if (firstUnlockedWeapon >= 0)
            Equip(firstUnlockedWeapon);

    }

    private void Update()
    {
        GetInput();
        Timers();
    }
    private void GetInput()
    {
        if (attackCD <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                Equip(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                Equip(1);


            if (Input.GetButton("Fire1"))
            {
                weaponAnimator.Play("Attack");
                switch (currentWeaponIndex)
                {

                    case 0: SwingSword(); break;
                    case 2: ThrowProtractor(); break;
                }
            }
        }
    }

    public void Equip(int index)
    {
        if (!(bool)SaveData.unlockedWeapons[index] || currentWeaponIndex == index) return;

        spriteRenderer.enabled = true;
        weaponAnimator.SetFloat("Weapon", index);
        weaponAnimator.Play("Equip");

        currentWeaponIndex = index;
        attackCD = 0.2f;
    }



    private void SwingSword()
    {
        attackCD = weapons[0].firingSpeed;
    }
    private void ThrowProtractor()
    {
        attackCD = weapons[1].firingSpeed;
    }

    private void Timers()
    {
        attackCD -= Time.deltaTime;
    }


}

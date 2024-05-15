using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] LayerMask collideWithRay;
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
                attackCD = weapons[currentWeaponIndex].firingSpeed;
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
        attackCD = 0.5f;
        GameObject projectile = Instantiate(protractorProjectile);
        projectile.transform.position = transform.position;
        if (projectile.GetComponent<Rigidbody>())
            projectile.GetComponent<Rigidbody>().AddForce(ProjectileDirection(transform.position) * 2500);
        Destroy(projectile, 2.5f);
    }

    private Vector3 ProjectileDirection(Vector3 shootPosition)
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 1000, collideWithRay))
            return (hit.point - shootPosition).normalized;    

        return (Camera.main.transform.forward * 1000 - shootPosition).normalized;
    }

    private void Timers()
    {
        attackCD -= Time.deltaTime;
    }


}

using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] public float damage;
    [SerializeField] public float projectileSpeed;
    [SerializeField] public float firingSpeed;
    [SerializeField] public WeaponType type;
}

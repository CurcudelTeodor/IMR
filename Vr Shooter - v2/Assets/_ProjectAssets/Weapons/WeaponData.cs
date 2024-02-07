using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int damage;
    public int magSize;
    public int fireSpeed;
    public float fireRate;
    public AudioClip fireSound;
}

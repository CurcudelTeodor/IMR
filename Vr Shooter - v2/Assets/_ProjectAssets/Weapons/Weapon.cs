using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;

    void Start()
    {
        Debug.Log("Weapon Name: " + weaponData.weaponName);
        Debug.Log("Damage: " + weaponData.damage);
        Debug.Log("Mag size: " + weaponData.magSize);
        Debug.Log("Fire Speed:" + weaponData.fireSpeed);

    }

    
}

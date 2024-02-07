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

        if (weaponData.fireSound != null)
        {
            Debug.Log("Fire Sound: " + weaponData.fireSound.name);
        }
        else
        {
            Debug.LogWarning("Fire Sound not assigned.");
        }
    }

    public float GetFireRate()
    {
        return weaponData.fireRate;
    }

}

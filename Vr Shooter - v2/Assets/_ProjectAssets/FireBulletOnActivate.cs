using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;

    public XRGrabInteractable grabbable;
    private Transform originalParent;  

    private bool isFiring = false;
    private ShakeWrapper shakeWrapper; // Reference to ShakeWrapper

    private Weapon weapon; // Reference to Weapon component

    // Start is called before the first frame update
    void Start()
    {
        //grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(StartFiring);
        grabbable.deactivated.AddListener(StopFiring);

        // Store the original parent of the gun
        originalParent = transform.parent;

        // Ensure the gun is a child of ShakeWrapper
        shakeWrapper = GetComponentInParent<ShakeWrapper>();

        // Get the Weapon component
        weapon = GetComponent<Weapon>();
    }


    // Update is called once per frame
    void Update()
    {
        // Continuously fire bullets while 'F' key is held down
        if (Input.GetKey(KeyCode.F) && grabbable.isSelected && !isFiring)
        {
            isFiring = true;
/*
            // Set the original parent of the gun to ShakeWrapper
            if (originalParent != null)
            {
                transform.parent = originalParent;
            }*/

            StartCoroutine(FireRoutine());
        }
        else if (!Input.GetKey(KeyCode.F) || !grabbable.isSelected)
        {
            StopFiring(null);
        }
    }

    void StartFiring(ActivateEventArgs arg)
    {
        isFiring = true;
    }

    void StopFiring(DeactivateEventArgs arg)
    {
        isFiring = false;
    }

    IEnumerator FireRoutine()
    {
        while (isFiring)
        {
            FireBullet(null);

            // Apply recoil using ShakeWrapper
            if (shakeWrapper != null)
            {
                shakeWrapper.ApplyRecoil();
            }

            // Retrieve the fire rate from the Weapon component
            float fireRate = weapon.GetFireRate();
            yield return new WaitForSeconds(1f / fireRate);
        }

        // Set the parent back to the original parent after firing
        /*if (transform.parent != originalParent)
        {
            transform.parent = originalParent;
        }*/
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        Debug.Log("Fired the bullet");

        Weapon weapon = GetComponent<Weapon>();
        if (weapon != null && weapon.weaponData != null)
        {
            AudioClip fireSound = weapon.weaponData.fireSound;
            if (fireSound != null)
            {
                AudioSource.PlayClipAtPoint(fireSound, transform.position);
            }
            else
            {
                Debug.LogWarning("Fire Sound not assigned in WeaponData.");
            }

            float fireSpeed = weapon.weaponData.fireSpeed;
            int damage = weapon.weaponData.damage;


            GameObject spawnedBullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            Bullet bulletComponent = spawnedBullet.GetComponent<Bullet>(); //get the Bullet script component

            // Apply the damage to the bullet component
            if (bulletComponent != null)
            {
                bulletComponent.damage = damage;
            }
            else
            {
                Debug.LogWarning("Bullet component not found on the spawned bullet.");
            }

            Rigidbody bulletRb = spawnedBullet.GetComponent<Rigidbody>();

            float randomAngle = Random.Range(-5f, 5f);
            Vector3 randomRotation = Quaternion.AngleAxis(randomAngle, spawnPoint.up) * spawnPoint.forward;
            bulletRb.velocity = randomRotation * fireSpeed;

            bulletRb.constraints = RigidbodyConstraints.FreezeRotation;
            Destroy(spawnedBullet, 10);
        }
        else
        {
            Debug.LogWarning("Weapon or WeaponData not found on the object.");
        }

/*         // If ShakeWrapper is the parent, set the parent back to the original parent
        if (transform.parent != originalParent)
        {
            transform.parent = originalParent;
        }*/
    }
}

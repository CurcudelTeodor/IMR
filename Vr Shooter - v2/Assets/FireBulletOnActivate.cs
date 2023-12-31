using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;

    private XRGrabInteractable grabbable;

    public float fireRate = 0.5f;
    private bool isFiring = false;

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(StartFiring);
        grabbable.deactivated.AddListener(StopFiring);
    }

    // Update is called once per frame
    void Update()
    {
        // Continuously fire bullets while 'F' key is held down
        if (Input.GetKey(KeyCode.F) && grabbable.isSelected && !isFiring)
        {
            isFiring = true;
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
            yield return new WaitForSeconds(1f / fireRate);
        }
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

            GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20;
    public AudioSource fireSound;

    private XRGrabInteractable grabbable;

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for input, for example, when the 'F' key is pressed
        if (Input.GetKeyDown(KeyCode.F) && grabbable.isSelected)
        {
            FireBullet(null);
        }
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        Debug.Log("Fired the bullet");
        if (fireSound != null)
        {
            fireSound.Play();
        }
        GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        Rigidbody bulletRb = spawnedBullet.GetComponent<Rigidbody>();

        float randomAngle = Random.Range(-5f, 5f);
        Vector3 randomRotation = Quaternion.AngleAxis(randomAngle, spawnPoint.up) * spawnPoint.forward;
        bulletRb.velocity = randomRotation * fireSpeed;

        bulletRb.constraints = RigidbodyConstraints.FreezeRotation;
        Destroy(spawnedBullet, 10);
    }

}

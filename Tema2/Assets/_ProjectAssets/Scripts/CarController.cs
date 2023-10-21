using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [SerializeField]
    float distanceFromSpawnPoint;
    bool spawnedNewCarAsDart = false;
    GameObject mainCamera;
    Vector3 spawnPosition;
    public GameObject score;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawnedNewCarAsDart && Vector3.Distance(spawnPosition, transform.position) > distanceFromSpawnPoint)
        {
            SpawnNewCar();
        }
        transform.LookAt(new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Military target")
        {   
            if (!spawnedNewCarAsDart)
            {   
                SpawnNewCar();
                ScoreScript.scoreValue += 1;
                ScoreScript.hitStatus = " yes -> " + ScoreScript.scoreValue;
                Debug.Log("Collided with " + collision.gameObject.name);
                
            }
            Destroy(gameObject);
        }
    }

    private void SpawnNewCar()
    {
        spawnedNewCarAsDart = true;
        GameObject.Find("CarSpawner").GetComponent<CarSpawnerController>().DartAsCar();
    }

}

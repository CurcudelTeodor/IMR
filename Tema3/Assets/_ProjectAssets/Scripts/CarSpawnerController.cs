using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSpawnerController : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        DartAsCar();
    }

    public void DartAsCar()
    {
        Instantiate(prefab, transform);
    }
}

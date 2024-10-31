using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject SpawnObject;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnObjectAtRandomPont();
        }
    }

    void SpawnObjectAtRandomPont()
    {
        float randomX = Random.Range(-1.5f, 1.5f);
        float randomZ = Random.Range(-15, 15);

        Vector3 randomPos = new Vector3(randomX, 1.16f, randomZ);
        Instantiate(SpawnObject, randomPos, Quaternion.identity);
    }
}

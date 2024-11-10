using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject SpawnObject;
    public Transform SpawnPoint;

    // SpawnCycleTime 간격마다 SpawnCount 만큼 Object 생성
    public float SpawnCycleTime = 2.0f;
    public int SpawnCount = 10;

    [SerializeField, Header("Debug")]
    private int _spawnedCount = 0;                  // 생성된 객체수 카운팅

    void Start()
    {
        StartCoroutine(Co_SpawnObject());
    }


    public void SpawnObjectAtRandomPont()
    {
        // SpawnPoint 값을 기준으로 근처에 랜덤 생성
        float randomX = Random.Range(-1.5f, 1.5f);
        float randomZ = Random.Range(-15, 15);

        Vector3 randomPos = new Vector3(SpawnPoint.position.x + randomX, 0.0f, SpawnPoint.position.z + randomZ);
        Instantiate(SpawnObject, randomPos, transform.rotation);

        _spawnedCount++;
    }

    IEnumerator Co_SpawnObject()
    {
        while (_spawnedCount < SpawnCount)
        {
            yield return new WaitForSeconds(SpawnCycleTime);
            SpawnObjectAtRandomPont();
        }
    }
}

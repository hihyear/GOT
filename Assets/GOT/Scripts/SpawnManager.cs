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

    // SpawnCycleTime ���ݸ��� SpawnCount ��ŭ Object ����
    public float SpawnCycleTime = 2.0f;
    public int SpawnCount = 10;

    [SerializeField, Header("Debug")]
    private int _spawnedCount = 0;                  // ������ ��ü�� ī����

    void Start()
    {
        StartCoroutine(Co_SpawnObject());
    }


    public void SpawnObjectAtRandomPont()
    {
        // SpawnPoint ���� �������� ��ó�� ���� ����
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

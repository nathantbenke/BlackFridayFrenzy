using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy prefab;


    public float minTime;
    public float maxTime;
    private void OnEnable()
    {
        StartCoroutine(CoSpawn());
    }

    IEnumerator CoSpawn()
    {
        while (true) // Infinite loop
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            Enemy e = Instantiate(prefab);
            e.transform.position = transform.position;
        }
    }
}

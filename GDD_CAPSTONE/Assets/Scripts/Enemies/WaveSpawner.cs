using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
   
    [SerializeField] GameObject[] wave;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (i <= wave.Length)
        {
            Instantiate(wave[i], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }

        Destroy(gameObject);
    }
}

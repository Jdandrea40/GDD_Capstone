using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeBase : MonoBehaviour
{
    [SerializeField] GameObject[] warningLights;
    IEnumerator bhCoRoutine;
    bool bhRunning = false;
    bool baseHit;
    private void Start()
    {
        bhCoRoutine = BaseHit();
        GameplayManager.Instance.BaseHealth = 100;
        warningLights[0].SetActive(false);
        warningLights[1].SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)CollisionLayers.ENEMIES)
        {
            //if (bhRunning)
            //{
            //    bhRunning = false;
            //    StopCoroutine(bhCoRoutine);
            //}

            //StartCoroutine(bhCoRoutine);
            GameplayManager.Instance.BaseHealth -= 10;
            
            
            
        }
    }

    IEnumerator BaseHit()
    {
        if (!baseHit)
        {
            baseHit = true;
            warningLights[0].SetActive(true);
            warningLights[1].SetActive(true);
            yield return new WaitForSeconds(.1f);
            warningLights[0].SetActive(false);
            warningLights[1].SetActive(false);
        }
        baseHit = false;
    }
}

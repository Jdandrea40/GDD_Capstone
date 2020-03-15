using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeBase : MonoBehaviour
{
    [SerializeField] GameObject[] warningLights;
    [SerializeField] SpriteRenderer[] warningRadius;
    IEnumerator bhCoRoutine;
    bool bhRunning = false;
    bool baseHit;

    Color startRed;
    Color fadeRed;
    private void Start()
    {
        startRed = new Color(255, 0, 0, .4f);
        fadeRed = new Color(255, 0, 0, .2f);
        bhCoRoutine = BaseHit();
        GameplayManager.Instance.BaseHealth = 100;
        warningLights[0].SetActive(false);
        warningLights[1].SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == (int)CollisionLayers.ENEMIES)
        {

                //StopCoroutine(bhCoRoutine);
            
            GameplayManager.Instance.BaseHealth -= 10;

            if (GameplayManager.Instance.BaseHealth > 50)
            {
                StartCoroutine(BaseHit());
            }
            else
            {
                StopCoroutine(BaseHit());
                StartCoroutine(LowHealth());
            }

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
    IEnumerator LowHealth()
    {
        if (!baseHit)
        {
            bool colorSwitch = true;
            Color cToBe = startRed;
            warningLights[0].SetActive(true);
            warningLights[1].SetActive(true);
            baseHit = true;
            while (true)
            {
                if (!GameplayManager.Instance.IsPaused)
                {
                    cToBe = (colorSwitch ? startRed : fadeRed);
                    warningRadius[0].color = cToBe;
                    warningRadius[1].color = cToBe;

                }
                else
                {
                    yield return new WaitUntil(() => GameplayManager.Instance.IsPaused == false);
                }
                yield return new WaitForSeconds(.3f);
                colorSwitch = !colorSwitch;
               
            }
        }
    }
}

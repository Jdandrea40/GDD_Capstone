using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All this does is destroy the particle effect after it runs (1 second)
/// </summary>
public class ParticleEffectDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KillMySelf());
    }


    IEnumerator KillMySelf()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }
}

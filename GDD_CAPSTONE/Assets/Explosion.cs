using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(Explode());
        
    }

    public void SetEffect(bool slow, bool DoT, int DoTAmount)
    {

    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}

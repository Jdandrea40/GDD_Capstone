using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_WaveIncoming : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyMyself());
        AudioManager.Instance.PlaySFX(AudioManager.Sounds.WAVE_WARNING);
    }

    IEnumerator DestroyMyself()
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLoader : MonoBehaviour
{
    private void Awake()
    {
        GameplayManager GM = GameplayManager.Instance;
        AudioManager AM = AudioManager.Instance;
        PiecesCollectedManager PCM = PiecesCollectedManager.Instance;

        if (GM != null && AM != null && PCM != null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameplayManager GM = GameplayManager.Instance;
        AudioManager AM = AudioManager.Instance;
        PiecesCollectedManager PCM = PiecesCollectedManager.Instance;

        if (GM != null && AM != null && PCM != null)
        {
            Destroy(gameObject);
        }
    }
}


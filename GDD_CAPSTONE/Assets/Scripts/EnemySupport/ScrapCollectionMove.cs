using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapCollectionMove : MonoBehaviour
{
    GameObject homeBase;
    //Transform target;
    Vector2 screenCent;
    ParticleSystem pSystem;
    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[5];
    int count;
    bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        homeBase = GameObject.FindGameObjectWithTag("Base");
        screenCent = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width/2, Screen.height + 100));
        pSystem = GetComponent<ParticleSystem>();
        StartCoroutine(WaitToMove());
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            count = pSystem.GetParticles(particles);
            for (int i = 0; i < count; i++)
            {
                ParticleSystem.Particle particle = particles[i];

                Vector2 v1 = pSystem.transform.TransformPoint(particle.position);
                Vector2 v2 = screenCent;

                Vector2 targetPos = (v2 - v1) * (particle.remainingLifetime / particle.startLifetime);
                particle.position = pSystem.transform.InverseTransformPoint(v2 - targetPos);
                particles[i] = particle;
            }

            pSystem.SetParticles(particles, count);
        }
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(.5f);
        canMove = true;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }
}

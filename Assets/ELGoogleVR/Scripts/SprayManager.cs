using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayManager : MonoBehaviour
{
    public ParticleSystem[] sprayParticleSystems;

    [Range(1000, 1000000)]
    public int sprayParticles = 1000;

    [Range(1.0f, 60.0f)]
    public float sprayPeriod = 1.0f;

    public AudioSource sfxSpray;

    private float[] initialSpeeds;
    private float initialSmallSpeed;
    private bool canSpray;
    private bool spraying;

    private void Awake()
    {
        initialSpeeds = new float[sprayParticleSystems.Length];

        for(int i = 0; i < initialSpeeds.Length; i++)
        {
            initialSpeeds[i] = sprayParticleSystems[i].startSpeed;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
            Spray();
    }

    public void Spray()
    {
        for (int i = 0; i < sprayParticleSystems.Length; i++)
        {
            canSpray = sprayParticleSystems[i].maxParticles - sprayParticleSystems[i].particleCount >= sprayParticles;
            //Debug.LogFormat("SprayManager -> Can Spray: {0}", canSpray);
            if (canSpray)
            {
                if(sfxSpray != null)
                {
                    sfxSpray.Play();
                }
           
                sprayParticleSystems[i].Emit(sprayParticles);
            }
        }
    }

    public void SprayLooping()
    {
        spraying = true;
        StartCoroutine(Spraying());
    }

    public void Stop()
    {
        spraying = false;
    }

    public void ResetSpraySpeed()
    {
        for (int i = 0; i < sprayParticleSystems.Length; i++)
        {
            sprayParticleSystems[i].startSpeed = initialSpeeds[i];
        }
    }

    public void ClearSpray()
    {
        for (int i = 0; i < sprayParticleSystems.Length; i++)
        {
            sprayParticleSystems[i].Clear();
        }
    }

    public void IncreaseSpraySpeed(float speedMultiplier)
    {
        for (int i = 0; i < sprayParticleSystems.Length; i++)
        {
            sprayParticleSystems[i].startSpeed = initialSpeeds[i] * speedMultiplier;
        }
    }

    IEnumerator Spraying()
    {
        while(spraying)
        {
            Spray();
            yield return new WaitForSeconds(sprayPeriod);
        }
    }
}

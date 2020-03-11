using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that loads all particles effects at the start of the game
/// Instantiate the particle in the calling class by
/// Instantiate(ParticleEffectManager.Instance.particleDictionary[ParticleEffectManager.ParticleToPlay.name]);
/// </summary>
public class ParticleEffectManager : Singleton<ParticleEffectManager>
{
    // Enumeration of Particles
    public enum ParticleToPlay 
    { 
        NORMAL_EXPLODE, 
        CRYO_EXPLODE, 
        INCENDIARY_EXPLODE 
    };

    public Dictionary<ParticleToPlay, ParticleSystem> particleDictionary = new Dictionary<ParticleToPlay, ParticleSystem>();

    private void AddParticlesToDictionary()
    {
        particleDictionary.Add(ParticleToPlay.NORMAL_EXPLODE, Resources.Load<ParticleSystem>("ParticleEffects/NormalExplode"));
        particleDictionary.Add(ParticleToPlay.CRYO_EXPLODE, Resources.Load<ParticleSystem>("ParticleEffects/CryoExplode"));
        particleDictionary.Add(ParticleToPlay.INCENDIARY_EXPLODE, Resources.Load<ParticleSystem>("ParticleEffects/IncendiaryExplode"));
    }
    // Called before Start()
    void Awake()
    {
        AddParticlesToDictionary();
        
    }
}

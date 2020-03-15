using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletionManager : Singleton<LevelCompletionManager>
{
    public enum LevelNames { DESERT_DEFENSE, THE_GRASSLANDS, ROUNDABOUT };
    public Dictionary<LevelNames, bool> completeledLevels = new Dictionary<LevelNames, bool>();
    public LevelNames currentLevel;

    private void Start()
    {
        completeledLevels[LevelNames.DESERT_DEFENSE] = false;
        completeledLevels[LevelNames.THE_GRASSLANDS] = false;
        completeledLevels[LevelNames.ROUNDABOUT] = false;
    }

}

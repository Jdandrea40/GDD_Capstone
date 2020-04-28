using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletionManager : Singleton<LevelCompletionManager>
{
    // TODO: Try Nested Dictionaries 1-> <Difficult<Dict<LevelName, bool>>
    // Easiest may be string comparison by name, can load in all levels at start, and then compare that way
    public enum LevelNames { GALLETCITY, THEFOREST, INTERSECTION, DEPOT };
    public Dictionary<LevelNames, bool> completeledLevels = new Dictionary<LevelNames, bool>();
    public LevelNames currentLevel;

    private void Start()
    {
        completeledLevels[LevelNames.GALLETCITY] = false;
        completeledLevels[LevelNames.THEFOREST] = false;
        completeledLevels[LevelNames.DEPOT] = false;
        completeledLevels[LevelNames.INTERSECTION] = false;

    }

}

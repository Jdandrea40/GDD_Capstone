using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    #region PROPERTIES

    static Enemy addEnemyTargetInvoker;
    static UnityAction<int, GameObject> addEnemyTargetListener;

    static Enemy removeEnemyTargetInvoker;
    static UnityAction<int, GameObject> removeEnemyTargetListener;

    static List<Tower> towerFireInvoker = new List<Tower>();
    static List<UnityAction<Transform>> towerFireListener = new List<UnityAction<Transform>>();
    
    #endregion

    #region EVENTS
    public static void AddEnemyTargetInvoker(Enemy invoker)
    {
        addEnemyTargetInvoker = invoker;
        if (addEnemyTargetListener != null)
        {
            invoker.AddEnemyTargetListener(addEnemyTargetListener);
        }
    }

    public static void AddEnemyTargetListener(UnityAction<int, GameObject> listener)
    {
        addEnemyTargetListener = listener;
        if (addEnemyTargetInvoker != null)
        {
            addEnemyTargetInvoker.AddEnemyTargetListener(listener);
        }
    }

    public static void RemoveEnemyTargetInvoker(Enemy invoker)
    {
        removeEnemyTargetInvoker = invoker;
        if (removeEnemyTargetListener != null)
        {
            invoker.RemoveEnemyTargetListener(addEnemyTargetListener);
        }
    }

    public static void RemoveEnemyTargetListener(UnityAction<int, GameObject> listener)
    {
        removeEnemyTargetListener = listener;
        if (removeEnemyTargetInvoker != null)
        {
            removeEnemyTargetInvoker.RemoveEnemyTargetListener(listener);
        }
    }

    public static void TowerFireInvoker(Tower invoker)
    {
        towerFireInvoker.Add(invoker);
        foreach (UnityAction<Transform> listener in towerFireListener)
        {
            //invoker.AddTowerFireListener(listener);
        }
    }

    public static void TowerFireListener(UnityAction<Transform> listener)
    {
        towerFireListener.Add(listener);
        foreach (Tower invoker in towerFireInvoker)
        {
            //invoker.AddTowerFireListener(listener);
        }
    }
    #endregion
}

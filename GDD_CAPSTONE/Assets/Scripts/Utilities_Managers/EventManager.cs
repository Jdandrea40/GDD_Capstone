using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    #region PROPERTIES

    static Enemy enemyDequeueInvoker;
    static UnityAction enemyDequeueListener;

    static List<Tower> towerFireInvoker = new List<Tower>();
    static List<UnityAction<Transform>> towerFireListener = new List<UnityAction<Transform>>();
    
    #endregion

    #region EVENTS
    public static void EnemyDequeueInvoker(Enemy invoker)
    {
        enemyDequeueInvoker = invoker;
        if(enemyDequeueListener != null)
        {
            invoker.AddEnemyDequeueListener(enemyDequeueListener);
        }
    }

    public static void EnemeyDequeueListener(UnityAction listener)
    {
        enemyDequeueListener = listener;
        if(enemyDequeueInvoker != null)
        {
            enemyDequeueInvoker.AddEnemyDequeueListener(listener);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    #region PROPERTIES

    static Enemy enemyDequeueInvoker;
    static UnityAction enemyDequeueListener;
    
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
    #endregion
}

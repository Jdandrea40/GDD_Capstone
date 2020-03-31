using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    #region PROPERTIES

    //static Enemy addEnemyTargetInvoker;
    //static UnityAction<int, GameObject> addEnemyTargetListener;

    //static Enemy removeEnemyTargetInvoker;
    //static UnityAction<int, GameObject> removeEnemyTargetListener;

    static List<Tower> towerFireInvoker = new List<Tower>();
    static List<UnityAction<int, bool, int, bool>> towerFireListener = new List<UnityAction<int, bool, int, bool>>();

    static HUDManager waveSpawnInvoker;
    static UnityAction waveSpawnListener;

    static HUD_BuyPanel itemBoughtInvoker;
    static UnityAction itemBoughtListener;

    static ItemDrop itemCollectedInvoker;
    static UnityAction itemCollectedListener;

    static Tower scrapUsedInvoker;
    static UnityAction scrapUsedListener;

    static Tower towerPanelInvoker;
    static UnityAction towerPanelListener;

    static Projectile enemyDamageInvoker;
    static UnityAction<int, bool, int, bool> enemyDamageListener;

    #endregion

    #region EVENTS

    #region Tower Panel
    public static void ActivateTowerPanelInvoker(Tower invoker)
    {
        towerPanelInvoker = invoker;
        if (towerPanelListener != null)
        {
            invoker.AddTowerPanelListener(towerPanelListener);
        }
    }

    public static void ActivateTowerPanelListener(UnityAction listener)
    {
        towerPanelListener = listener;
        if (towerPanelInvoker != null)
        {
            towerPanelInvoker.AddTowerPanelListener(listener);
        }
    }
    #endregion
    #region Scrap Used
    /// <summary>
    /// This event is being used to update the UI After a tower has been built
    /// this will determine if the user is still able to select pieces to craft with
    /// </summary>
    /// <param name="invoker"></param>
    public static void ScrapUsedInvoker(Tower invoker)
    {
        scrapUsedInvoker = invoker;
        if (scrapUsedListener != null)
        {
            invoker.AddScrapUsedListener(scrapUsedListener);
        }
    }

    public static void ScrapUsedListener(UnityAction listener)
    {
        scrapUsedListener = listener;
        if (scrapUsedInvoker != null)
        {
            scrapUsedInvoker.AddScrapUsedListener(listener);
        }
    }

    #endregion
    public static void AddItemBoughtInvoker(HUD_BuyPanel invoker)
    {
        itemBoughtInvoker = invoker;
        if (itemCollectedListener != null)
        {
            invoker.AddItemBoughtListener(itemCollectedListener);
        }
    }

    public static void AddItemBoughtListener(UnityAction listener)
    {
        itemBoughtListener = listener;
        if (itemBoughtInvoker != null)
        {
            itemBoughtInvoker.AddItemBoughtListener(listener);
        }
    }
    #region Item Pickup
    /// <summary>
    /// This event is being used to update the UI when an Item (scrap)
    /// is collected. Will update the text of the specific item collected
    /// </summary>
    /// <param name="invoker"></param>
    public static void AddItemCollectedInvoker(ItemDrop invoker)
    {
        itemCollectedInvoker = invoker;
        if (itemCollectedListener != null)
        {
            invoker.AddItemCollectedListener(itemCollectedListener);
        }
    }


    public static void AddItemCollectedListener(UnityAction listener)
    {
        itemCollectedListener = listener;
        if (itemCollectedInvoker != null)
        {
            itemCollectedInvoker.AddItemCollectedListener(listener);
        }
    }

    #endregion
    public static void AddWaveSpawnInvoker(HUDManager invoker)
    {
        waveSpawnInvoker = invoker;
        if(waveSpawnListener != null)
        {
            invoker.AddWaveSpawnListener(waveSpawnListener);
        }
    }

    public static void AddWaveSpawnListener(UnityAction listener)
    {
        waveSpawnListener = listener;
        if (waveSpawnInvoker != null)
        {
            waveSpawnInvoker.AddWaveSpawnListener(listener);
        }
    }

    //public static void AddEnemyTargetInvoker(Enemy invoker)
    //{
    //    addEnemyTargetInvoker = invoker;
    //    if (addEnemyTargetListener != null)
    //    {
    //        invoker.AddEnemyTargetListener(addEnemyTargetListener);
    //    }
    //}

    //public static void AddEnemyTargetListener(UnityAction<int, GameObject> listener)
    //{
    //    addEnemyTargetListener = listener;
    //    if (addEnemyTargetInvoker != null)
    //    {
    //        addEnemyTargetInvoker.AddEnemyTargetListener(listener);
    //    }
    //}

    //public static void RemoveEnemyTargetInvoker(Enemy invoker)
    //{
    //    removeEnemyTargetInvoker = invoker;
    //    if (removeEnemyTargetListener != null)
    //    {
    //        invoker.RemoveEnemyTargetListener(addEnemyTargetListener);
    //    }
    //}

    //public static void RemoveEnemyTargetListener(UnityAction<int, GameObject> listener)
    //{
    //    removeEnemyTargetListener = listener;
    //    if (removeEnemyTargetInvoker != null)
    //    {
    //        removeEnemyTargetInvoker.RemoveEnemyTargetListener(listener);
    //    }
    //}

    //public static void AddEnemyDamageInvoker(Projectile invoker)
    //{
    //    enemyDamageInvoker = invoker;
    //    if (enemyDamageListener != null)
    //    {
    //        invoker.AddEnemyDamageListener(enemyDamageListener);
    //    }
    //}

    //public static void AddEnemyDamageListener(UnityAction<int, bool, int, bool> listener)
    //{
    //    enemyDamageListener = listener;
    //    if (enemyDamageListener != null)
    //    {
    //        enemyDamageInvoker.AddEnemyDamageListener(listener);
    //    }
    //}

    //public static void TowerFireInvoker(Tower invoker)
    //{
    //    towerFireInvoker.Add(invoker);
    //    foreach (UnityAction<int, bool, int, bool> listener in towerFireListener)
    //    {
    //        invoker.AddTowerFireListener(listener);
    //    }
    //}

    //public static void TowerFireListener(UnityAction<int, bool, int, bool> listener)
    //{
    //    towerFireListener.Add(listener);
    //    foreach (Tower invoker in towerFireInvoker)
    //    {
    //        invoker.AddTowerFireListener(listener);
    //    }
    //}
    #endregion
}

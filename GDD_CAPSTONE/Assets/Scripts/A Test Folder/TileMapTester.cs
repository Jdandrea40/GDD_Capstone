using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TileMapTester : MonoBehaviour
{
    [SerializeField] GameObject build;
    [SerializeField] Tile builtTile;
    [SerializeField] Tilemap tilemp;

    Vector3Int selectedTile;

    void Update() 
    { 
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (GameplayManager.Instance.CanBuildArea)
        {       
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            selectedTile = tilemp.WorldToCell(point);
            // Will show an error cursor over unbuildable areas
            if (!tilemp.ContainsTile(tilemp.GetTile(selectedTile)))
            {
                ArtManager.Instance.SwapCursor(ArtManager.CursorToUse.ERROR);
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (tilemp.ContainsTile(tilemp.GetTile(selectedTile)) && tilemp.GetTile(selectedTile) != builtTile)
                {
                    GameplayManager.Instance.ScrapCollected -= 25;
                    tilemp.SetTile(selectedTile, builtTile);
                    Instantiate(build, new Vector2(selectedTile.x + .5f, selectedTile.y + .5f), Quaternion.identity);

                }


            }
        }
    }
}

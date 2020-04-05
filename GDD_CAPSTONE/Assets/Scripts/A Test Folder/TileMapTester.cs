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
    Color white;
    Color black;
    void Start() 
    { 
        //tilemp = GameObject.Find("EnemyPath").GetComponent<Tilemap>();

        white = new Color(1, 1, 1, 1);
        black = new Color(100, 100, 100, 1);
    }
    void Update() 
    { 
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        
        if (Input.GetMouseButtonDown(0) && GameplayManager.Instance.CanBuildArea)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            selectedTile = tilemp.WorldToCell(point);
            
            if (tilemp.ContainsTile(tilemp.GetTile(selectedTile)) && tilemp.GetTile(selectedTile) != builtTile)
            {
                GameplayManager.Instance.ScrapCollected -= 25;
                tilemp.SetTile(selectedTile, builtTile);
                Instantiate(build, new Vector2(selectedTile.x + .5f, selectedTile.y + .5f), Quaternion.identity);
                
            }


        }
    }

    private void OnMouseEnter()
    {
        if (tilemp.ContainsTile(tilemp.GetTile(selectedTile)) && tilemp.GetTile(selectedTile) != builtTile)
        {
            Debug.Log("HOVER");
        }
    }


    //private void OnMouseEnter()
    //{
    //    gameObject.SetActive(false);
    //}
}

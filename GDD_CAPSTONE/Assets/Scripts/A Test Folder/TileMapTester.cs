using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapTester : MonoBehaviour
{
    [SerializeField] GameObject build;
    [SerializeField] Tile hoverTile;
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

        
        if (Input.GetMouseButtonDown(0))
        {
            selectedTile = tilemp.WorldToCell(point);
            
            if (tilemp.ContainsTile(tilemp.GetTile(selectedTile)) && tilemp.GetTile(selectedTile) != builtTile)
            {
                tilemp.SetTile(selectedTile, builtTile);
                Instantiate(build, new Vector2(selectedTile.x + .5f, selectedTile.y + .5f), Quaternion.identity);
                Debug.Log("Poop");
            }


        }
    }

    private void OnMouseEnter()
    {
        if (tilemp.ContainsTile(tilemp.GetTile(selectedTile)) && tilemp.GetTile(selectedTile) != builtTile)
        {
            tilemp.SetTile(selectedTile,hoverTile);
            Debug.Log("HOVER");
        }
    }
    private void OnMouseExit()
    {
        tilemp.SetTile(selectedTile, builtTile);
    }

    //private void OnMouseEnter()
    //{
    //    gameObject.SetActive(false);
    //}
}

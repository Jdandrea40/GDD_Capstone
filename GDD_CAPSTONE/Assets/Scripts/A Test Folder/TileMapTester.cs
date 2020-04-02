using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapTester : MonoBehaviour
{
    [SerializeField] GameObject build;
    Tilemap tilemp;
    void Start() 
    { 
        tilemp = GameObject.Find("Grass").GetComponent<Tilemap>();
        Debug.Log(tilemp.ToString());
    }
    void Update() 
    { 
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int selectedTile = tilemp.WorldToCell(point);
            Debug.Log(tilemp.GetTile(selectedTile));
            if (tilemp.ContainsTile(tilemp.GetTile(selectedTile)))
            {
                Instantiate(build, new Vector2(selectedTile.x + 1, selectedTile.y + 1), Quaternion.identity);
            }


        }
    }

    //private void OnMouseEnter()
    //{
    //    gameObject.SetActive(false);
    //}
}

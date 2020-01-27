using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTopDisplay : MonoBehaviour
{
    [SerializeField] TurretTop[] turretTop;
    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
       int randomPieceNum = Random.Range(0, turretTop.Length);
        Spawn(randomPieceNum);
    }

    void Spawn(int selectedPiece)
    {
        sr.sprite = turretTop[selectedPiece].TurretSprite;
    }
}

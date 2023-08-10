using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureCanvas : MonoBehaviour
{
    public int piecesAquired;
    public Sprite[] CanvasImagesParts;
    public void NewPieceAquired(){
        piecesAquired += 1;
        gameObject.GetComponent<SpriteRenderer>().sprite = CanvasImagesParts[piecesAquired];
    }
}

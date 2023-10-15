using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armaDropped : MonoBehaviour
{
    public Sprite armaSprite, miraSprite, armaSlot;
    public float posY;
    public arma.tipoArma armaAtt;
    public AudioClip suonoSparo;
    public proiettile.tipo type;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<arma>())
        {
            col.GetComponent<arma>().armaCol = GetComponent<Collider2D>();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<arma>())
        {
            col.GetComponent<arma>().armaCol = null;
        }
    }
}
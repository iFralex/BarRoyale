using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proiettile : MonoBehaviour
{
    public float danno, dannoSec, velocità, secondi;
    public playerManager proprietario;
    public bool dist;
    public enum tipo { proiettile, laser }
    public tipo type;

    void Start()
    {
        if (dist)
        {
            StartCoroutine(Distruggi());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (proprietario != col.gameObject)
        {
            if (col.gameObject.tag == "Player" || col.gameObject.tag == "da colpire")
            {
                if (type == tipo.proiettile)
                {
                    col.GetComponent<Vita>().CausaDanno(danno);
                    col.GetComponent<Vita>().StartCoroutine(col.GetComponent<Vita>().CambiaColore(Color.red, .1f));
                    if (dist)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (type == tipo.laser)
        {
            if (proprietario != col.gameObject)
            {
                if (col.gameObject.tag == "Player" || col.gameObject.tag == "da colpire")
                {
                    col.GetComponent<Vita>().CambiaColor(Color.red);
                    col.GetComponent<Vita>().CausaDanno(dannoSec * Time.deltaTime);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (type == tipo.laser)
        {
            if (proprietario != col.gameObject)
            {
                if (col.gameObject.tag == "Player" || col.gameObject.tag == "da colpire")
                {
                    col.GetComponent<Vita>().CambiaColor(Color.white);
                }
            }
        }
    }

    IEnumerator Distruggi()
    {
        yield return new WaitForSeconds(secondi);
        Destroy(gameObject);
    }
}
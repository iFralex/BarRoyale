using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vita : MonoBehaviour
{
    arma armaSc;
    public float vita, vitaTotale, shield, shieldTotale;
    public UnityEngine.UI.Image barraVita, barraShield;
    public Transform barraVitaNem;
    public bool dist;

    void Start()
    {
        armaSc = GetComponent<arma>();
    }

    public void CausaDanno(float vitaMeno)
    {
        if (shield <= 0)
        {
            float n = vita - vitaMeno;
            if (n > 0)
            {
                vita -= vitaMeno;
            }
            else
            {
                vita = 0;
                if (dist)
                {
                    Destroy(gameObject);
                }
            }

            if (barraVita != null | barraVitaNem != null)
            {
                if (GetComponentInChildren<arma>())
                {
                    if (!GetComponentInChildren<arma>().nemico)
                    {
                        barraVita.rectTransform.localScale = new Vector3(vita / vitaTotale, 1, 1);
                    }
                    else
                    {
                        barraVitaNem.localScale = new Vector3(vita / vitaTotale, barraVitaNem.localScale.y, barraVitaNem.localScale.z);
                    }
                }
                else
                {
                    barraVitaNem.localScale = new Vector3(vita / vitaTotale, barraVitaNem.localScale.y, barraVitaNem.localScale.z);
                }
            }
        }
        else
        {
            float n = shield - vitaMeno;
            if (n > 0)
            {
                shield -= vitaMeno;
            }
            else
            {
                shield = 0;
            }

            if (barraShield != null)
            {
                barraShield.rectTransform.localScale = new Vector3(shield / shieldTotale, 1, 1);
            }
        }
    }

    public IEnumerator CambiaColore(Color color, float seconds)
    {
        CambiaColor(color);
        yield return new WaitForSeconds(seconds);
        CambiaColor(Color.white);
    }

    public void CambiaColor(Color color)
    {
        if (transform.Find("cappello"))
        {
            transform.Find("cappello").GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = color;
        }
        if (GetComponent<arma>())
        {
            GetComponent<arma>().armaPlayer.GetComponent<SpriteRenderer>().color = color;
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }*/

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderBarra : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (GetComponentInParent<arma>().barraNoCollide)
        {
            GetComponentInParent<arma>().barraNoCollide = false;
            GetComponentInParent<arma>().mira.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        GetComponentInParent<arma>().barraNoCollide = true;
        GetComponentInParent<arma>().mira.GetComponent<SpriteRenderer>().color = Color.white;
    }
}

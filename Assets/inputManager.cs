using System.Collections;
using UnityEngine;

public class inputManager : MonoBehaviour
{
    public RectTransform areaMov, pomelloMov, areaMir, pomelloMir;
    public float raggio;
    public int toccoMov = -1, toccoMir = -1;
    float schermo;
    bool premuto;

    void Start()
    {
        raggio = Screen.height * 150 / 1080;
        schermo = Screen.width / 2;
        UnityEngine.UI.Button[] bottoni = FindObjectsOfType<UnityEngine.UI.Button>();
        for (int i = 0; i < bottoni.Length; i++)
        {
            //bottoni[i].onClick.AddListener(AttivaPulsante);
        }
    }

    void Update()
    {
        if (!premuto)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    if (Input.GetTouch(i).position.x < schermo)
                    {
                        toccoMov = i;
                    }
                    else
                    {
                        toccoMir = i;
                    }
                }

                if (toccoMov == i)
                {
                    switch (Input.GetTouch(i).phase)
                    {
                        case TouchPhase.Began:
                            areaMov.position = Input.GetTouch(i).position;
                            pomelloMov.position = areaMov.position;
                            pomelloMov.gameObject.SetActive(true);
                            areaMov.gameObject.SetActive(true);
                            transform.Find("cappello").GetComponent<Animator>().SetBool("cammina", true);
                            break;
                        case TouchPhase.Moved:
                            pomelloMov.position = Input.GetTouch(i).position;
                            Vector2 dist = pomelloMov.position - areaMov.position;
                            if (Mathf.Abs(dist.magnitude) > 100)
                            {
                                areaMov.position = new Vector2(Mathf.Clamp(areaMov.position.x, pomelloMov.position.x - raggio, pomelloMov.position.x + raggio), Mathf.Clamp(areaMov.position.y, pomelloMov.position.y - raggio, pomelloMov.position.y + raggio));
                            }
                            break;
                        case TouchPhase.Ended:
                            pomelloMov.gameObject.SetActive(false);
                            areaMov.gameObject.SetActive(false);
                            transform.Find("cappello").GetComponent<Animator>().SetBool("cammina", false);
                            break;
                    }
                }
                else
                {
                    if (toccoMir == i)
                    {
                        switch (Input.GetTouch(i).phase)
                        {
                            case TouchPhase.Began:
                                areaMir.position = Input.GetTouch(i).position;
                                pomelloMir.position = areaMir.position;
                                pomelloMir.gameObject.SetActive(true);
                                areaMir.gameObject.SetActive(true);
                                if (GetComponent<arma>().armaAttuale == arma.tipoArma.icelaser)// | GetComponent<arma>().armaAttuale == arma.armaSel.firepump)
                                {
                                    GetComponent<arma>().staSparando = true;
                                    GetComponent<arma>().Spara();
                                }
                                break;
                            case TouchPhase.Moved:
                                pomelloMir.position = Input.GetTouch(i).position;
                                Vector2 dist = pomelloMov.position - areaMov.position;
                                if (Mathf.Abs(dist.magnitude) > 100)
                                {
                                    areaMir.position = new Vector2(Mathf.Clamp(areaMir.position.x, pomelloMir.position.x - raggio, pomelloMir.position.x + raggio), Mathf.Clamp(areaMir.position.y, pomelloMir.position.y - raggio, pomelloMir.position.y + raggio));
                                }
                                break;
                            case TouchPhase.Ended:
                                pomelloMir.gameObject.SetActive(false);
                                areaMir.gameObject.SetActive(false);
                                if (GetComponent<arma>().armaAttuale == arma.tipoArma.icelaser)// | GetComponent<arma>().armaAttuale == arma.armaSel.firepump)
                                {
                                    GetComponent<arma>().staSparando = false;
                                    //GetComponent<arma>().Spara();
                                }
                                Animator animatoreArma = GetComponent<arma>().armaPlayer.GetComponent<Animator>();
                                if (!animatoreArma.transform.Find("sparo").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("spara") && !animatoreArma.GetCurrentAnimatorStateInfo(0).IsTag("katana"))
                                {
                                    GetComponent<arma>().Spara();
                                }
                                break;
                        }
                    }
                }

                if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    if (toccoMov == i)
                    {
                        toccoMov = -1;
                    }
                    else
                    {
                        toccoMir = -1;
                    }

                    if (i == 0 && Input.touchCount > 1)
                    {
                        if (toccoMov == 1)
                        {
                            toccoMov = 0;
                        }
                        if (toccoMir == 1)
                        {
                            toccoMir = 0;
                        }
                    }
                }
            }
        }/*
        else
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    premuto = false;
                }
            }
        }*/
    }

    public void AttivaPulsante()
    {
        if (!premuto)
        {
            premuto = true;
        }
        else
        {
            premuto = false;
        }
    }
}
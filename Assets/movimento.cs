using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimento : MonoBehaviour
{
    public inputManager im;
    Rigidbody2D rg;
    public Transform raggioSparo;
    public float velocità;
    Animator animazioneArma, animazioneSparo;

    void Start()
    {
#if UNITY_EDITOR
        Camera.main.orthographicSize = 10;
#endif
        rg = GetComponent<Rigidbody2D>();
        im = GetComponent<inputManager>();
        animazioneArma = GetComponent<arma>().armaPlayer.GetComponent<Animator>();
        animazioneSparo = animazioneArma.transform.Find("sparo").GetComponent<Animator>();
    }

    public void CameraLontana(string valore)
    {
        Camera.main.orthographicSize = Convert.ToInt32(valore);
    }
    void Update()
    {
        Camera.main.transform.position = transform.position;
        /*for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Moved)
            {
                if (Input.GetTouch(i).position.x < 0 && !moviment)
                {
                    moviment = true;
                    joystick.OnDrag(Input.GetTouch(i));
                }
                else
                {
                    if (Input.GetTouch(i).position.x > 0 && !mira)
                    {
                        mira = true;
                        joystickMira.OnDrag(Input.GetTouch(i));
                    }
                }
            }
            else
            {
                if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    if (Input.GetTouch(i).position.x < 0 && moviment)
                    {
                        moviment = false;
                        joystick.Fine(Input.GetTouch(i));
                    }
                    else
                    {
                        if (Input.GetTouch(i).position.x > 0 && mira)
                        {
                            mira = false;
                            joystickMira.Fine(Input.GetTouch(i));
                        }
                    }
                }
            }
        }
    */
        if (im.areaMir.gameObject.activeInHierarchy)
        {
            Vector2 dir = im.pomelloMir.position - im.areaMir.position;
            float modulo = dir.magnitude;
            if (modulo != 0)
            {
                if (!raggioSparo.gameObject.activeInHierarchy)
                {
                    raggioSparo.gameObject.SetActive(true);
                }

                float direzione = 0;
                if (dir.y > 0)
                {
                    direzione = (Mathf.Acos(dir.x / modulo) * Mathf.Rad2Deg) + 270;
                }
                else
                {
                    direzione = (Mathf.Acos(-dir.x / modulo) * Mathf.Rad2Deg) + 90;
                }
                transform.eulerAngles = new Vector3(0, 0, direzione);
            }
        }
        else if (raggioSparo.gameObject.activeInHierarchy)
        {
            raggioSparo.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (im.areaMov.gameObject.activeInHierarchy)
        {
            Vector2 dir = im.pomelloMov.position - im.areaMov.position;
            float modulo = (im.pomelloMov.position - im.areaMov.position).magnitude;
            if (modulo != 0)
            {
                if (!raggioSparo.gameObject.activeInHierarchy)
                {
                    if (!animazioneArma.GetCurrentAnimatorStateInfo(0).IsTag("katana") && !animazioneSparo.GetCurrentAnimatorStateInfo(0).IsTag("spara") && !GetComponent<arma>().staSparando)
                    {
                        float direzione = 0;
                        if (dir.y > 0)
                        {
                            direzione = (Mathf.Acos(dir.x / modulo) * Mathf.Rad2Deg) + 270;
                        }
                        else
                        {
                            direzione = (Mathf.Acos(-dir.x / modulo) * Mathf.Rad2Deg) + 90;
                        }
                        transform.eulerAngles = new Vector3(0, 0, direzione);
                    }
                }
                rg.velocity = new Vector2(dir.x / im.raggio, dir.y / im.raggio) * velocità;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arma : MonoBehaviour
{
    public Sprite miaArma, slotVuoto, slotVuotoShield, slotVuotoKit;
    public SpriteRenderer armaPlayer, mira;
    public UnityEngine.UI.Image[] slots;
    public UnityEngine.UI.Image slotBarra, slotShield, slotKit;
    public enum tipoArma { firepump, katana, balestra, icelaser, barra, shield, kit}
    public tipoArma armaAttuale;
    public GameObject armaDrpoPrefab;
    public Transform armaDropPos, barraPos;
    public GameObject[] proiettiliPrefab;
    public Collider2D armaCol;
    slotArma slotSel;
    public bool staSparando, barraNoCollide = true, nemico;
    public Transform[] direzioniSparo;
    playerManager pm;
    public float potenza = 1, vitaBarra = 1;

    private void Start()
    {
        pm = GetComponent<playerManager>();
        if (nemico)
        {
            StartCoroutine(sparaContinuo());
        }
        else
        {
            slotSel = slots[0].GetComponent<slotArma>();
        }
    }

    public void RaccogliArma()
    {
        if (armaCol != null)
        {
            void Raccogli(slotArma slotSelezionato)
            {
                armaDropped armaDroppato = armaCol.GetComponent<armaDropped>();
                slotSelezionato.armaLegata = armaCol.GetComponent<armaDropped>().armaSprite;
                slotSelezionato.GetComponent<UnityEngine.UI.Image>().sprite = armaDroppato.armaSlot;
                slotSelezionato.miraSprite = armaDroppato.miraSprite;
                slotSelezionato.miraPosY = armaDroppato.posY;
                slotSelezionato.armaAt = armaDroppato.armaAtt;
                slotSelezionato.armaDropped = armaCol.GetComponent<SpriteRenderer>().sprite;
                slotSelezionato.GetComponent<UnityEngine.UI.Button>().interactable = true;
                slotSelezionato.suonoSparo = armaDroppato.suonoSparo;
                slotSelezionato.type = armaDroppato.type;
                Destroy(armaCol.gameObject);
            }

            if (armaCol.GetComponent<armaDropped>().armaAtt == tipoArma.shield)
            {
                Transform slot = slotShield.transform.GetChild(0);
                string stringShield = slot.GetComponent<UnityEngine.UI.Text>().text;
                string[] numeroShield = stringShield.Split(char.Parse("x"));
                int n = System.Convert.ToInt32(numeroShield[1]);
                if (n < 2)
                {
                    Raccogli(slotShield.GetComponent<slotArma>());
                    AggiungiASlotShieldOKit(slot, 1, slotVuotoShield);
                }
            }
            else if (armaCol.GetComponent<armaDropped>().armaAtt == tipoArma.kit)
            {
                Transform slot = slotKit.transform.GetChild(0);
                string stringShield = slot.GetComponent<UnityEngine.UI.Text>().text;
                string[] numeroShield = stringShield.Split(char.Parse("x"));
                int n = System.Convert.ToInt32(numeroShield[1]);
                if (n < 2)
                {
                    Raccogli(slotKit.GetComponent<slotArma>());
                    AggiungiASlotShieldOKit(slotKit.transform.GetChild(0), 1, slotVuotoKit);
                }
            }
            else
            {
                List<Sprite> armiInSlot = new List<Sprite>();
                for (int o = 0; o < slots.Length; o++)
                {
                    if (slots[o].GetComponent<slotArma>().armaLegata != null)
                    {
                        armiInSlot.Add(slots[o].GetComponent<slotArma>().armaLegata);
                    }
                }

                if (!armiInSlot.Contains(armaCol.GetComponent<armaDropped>().armaSprite))
                {
                    for (int i = 0; i < slots.Length; i++)
                    {
                        if (slots[i].GetComponent<slotArma>().armaLegata == null)
                        {
                            if (armiInSlot.Count <= slots.Length)
                            {
                                Raccogli(slots[i].GetComponent<slotArma>());
                            }
                            break;
                        }
                        else
                        {
                            if (i == slots.Length - 1)
                            {
                                if (armaAttuale != tipoArma.barra)
                                {
                                    DropArma();
                                    Raccogli(slotSel);
                                    SelezionaArma(-1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void SelezionaArma(int i)
    {
        slotArma slotSellezionato;
        if (i == -1)
        {
            slotSellezionato = slotSel;
        }
        else
        {
            if (i == -2)
            {
                slotSellezionato = slotBarra.GetComponent<slotArma>();
                slotSel = slotSellezionato;
            }
            else
            {
                slotSellezionato = slots[i].GetComponent<slotArma>();
                slotSel = slotSellezionato;
            }
        }
        GetComponent<AudioSource>().clip = slotSellezionato.suonoSparo;
        armaPlayer.sprite = slotSellezionato.armaLegata;
        mira.sprite = slotSellezionato.miraSprite;
        mira.transform.localPosition = new Vector2(0, slotSellezionato.miraPosY);
        armaAttuale = slotSellezionato.armaAt;
        armaPlayer.GetComponent<proiettile>().type = slotSellezionato.type;
        armaPlayer.transform.Find("sparo").GetComponent<proiettile>().type = slotSellezionato.type;
    }

    public void DropArma()
    {
        armaDropped armaDroppata = Instantiate(armaDrpoPrefab, armaDropPos.position, Quaternion.identity).GetComponent< armaDropped>();
        armaDroppata.GetComponent<SpriteRenderer>().sprite = slotSel.armaDropped;
        armaDroppata.armaSprite = slotSel.armaLegata;
        armaDroppata.posY = slotSel.miraPosY;
        armaDroppata.miraSprite = slotSel.miraSprite;
        armaDroppata.armaSlot = slotSel.GetComponent<UnityEngine.UI.Image>().sprite;
        armaDroppata.armaAtt = slotSel.armaAt;
        armaDroppata.suonoSparo = slotSel.suonoSparo;
        armaDroppata.type = slotSel.type;
        slotSel.GetComponent<UnityEngine.UI.Button>().interactable = false;
        //slotSel.GetComponent<slotArma>().armaLegata = null;
        //slotSel.sprite = slotVuoto;
    }
    int[] Rot()
    {
        int[] rot = new int[5];
        rot[1] = 15;
        rot[2] = -15;
        //rot[3] = -5;
        //rot[4] = 5;
        return rot;
    }

    public void Spara()
    {
        switch (armaAttuale)
        {
            case tipoArma.balestra:
                GameObject proiettil = Instantiate(proiettiliPrefab[(int)tipoArma.balestra], mira.transform.position, transform.rotation);
                proiettil.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * proiettil.GetComponent<proiettile>().velocità, ForceMode2D.Impulse);
                proiettil.GetComponent<proiettile>().danno *= potenza;
                if (!nemico)
                {
                    proiettil.GetComponent<proiettile>().proprietario = pm;
                }
                else
                {
                    proiettil.GetComponent<proiettile>().proprietario = transform.parent.gameObject.GetComponent<playerManager>();
                }
                break;
            case tipoArma.firepump:
                //armaPlayer.transform.Find("sparo").GetComponent<Animator>().SetTrigger("spara firepump");
                //if (staSparando)
                //{
                    StartCoroutine(sparaProiettili(Rot(), .1f));
                //}
                break;
            case tipoArma.icelaser:
                armaPlayer.transform.Find("sparo").GetComponent<Animator>().SetBool("spara icelaser", staSparando);
                armaPlayer.transform.Find("sparo").GetComponent<proiettile>().dannoSec *= potenza;
                break;
            case tipoArma.katana:
                armaPlayer.GetComponent<Animator>().SetTrigger("spara katana");
                armaPlayer.GetComponent<proiettile>().danno *= potenza;
                break;
            case tipoArma.barra:
                if (barraNoCollide && pm.nBarre > 0)
                {
                    GameObject barra = Instantiate(proiettiliPrefab[(int)tipoArma.barra], barraPos.position, transform.rotation);
                    barra.GetComponent<Vita>().vita *= vitaBarra;
                    pm.nBarre--;
                    UIPlayer uip = GetComponent<UIPlayer>();
                    uip.AggiornaValori(uip.barreCont, pm.nBarre);
                }
                break;
        }
        if (!nemico)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    IEnumerator sparaContinuo()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(1);
            if (armaPlayer.transform.Find("sparo") != null && armaPlayer.transform.Find("sparo").GetComponent<proiettile>().type == proiettile.tipo.laser)
            {
                if (staSparando)
                {
                    staSparando = false;
                }
                else
                {
                    staSparando = true;
                }
            }
            //Spara();
        }
    }

    void AggiungiASlotShieldOKit(Transform slot, int valore, Sprite spriteVuoto)
    {
        if (!slot.gameObject.activeInHierarchy)
        {
            slot.gameObject.SetActive(true);
            slot.GetComponent<UnityEngine.UI.Text>().text = "x1";
            slot.parent.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        else
        {
            string stringShield = slot.GetComponent<UnityEngine.UI.Text>().text;
            string[] numeroShield = stringShield.Split(char.Parse("x"));
            int n = System.Convert.ToInt32(numeroShield[1]);
            if (n + valore <= 2)
            {
                slot.GetComponent<UnityEngine.UI.Text>().text = "x" + (n + valore).ToString();
                if (n + valore <= 0)
                {
                    slot.gameObject.SetActive(false);
                    slot.parent.GetComponent<UnityEngine.UI.Button>().interactable = false;
                    slot.parent.GetComponent<UnityEngine.UI.Image>().sprite = spriteVuoto;
                }
            }
        }
    }

    public void SelezionaShieldOKit(int scelta)
    {
        if (scelta == 1)
        {
            AggiungiASlotShieldOKit(slotShield.transform.GetChild(0), -1, slotVuotoShield);
            if (GetComponent<Vita>().shield + (GetComponent<Vita>().shieldTotale / 2) >= GetComponent<Vita>().shieldTotale)
            {
                GetComponent<Vita>().shield = GetComponent<Vita>().shieldTotale;
            }
            else
            {
                GetComponent<Vita>().shield += 5;
            }
            GetComponent<Vita>().CausaDanno(0);
        }
        else if (scelta == 2)
        {
            AggiungiASlotShieldOKit(slotKit.transform.GetChild(0), -1, slotVuotoKit);
            GetComponent<Vita>().vita = GetComponent<Vita>().vitaTotale;
            GetComponent<Vita>().barraVita.rectTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    public IEnumerator sparaProiettili(int[] rotazioni, float _secondi)
    {
        staSparando = true;
        for (int i = 0; i < rotazioni.Length; i++)
        {
            direzioniSparo[i].localEulerAngles = new Vector3(0, 0, rotazioni[i]);
        }

        for (int o = 0; o < 3; o++)
        {
            for (int i = 0; i < rotazioni.Length; i++)
            {
                GameObject proiettil = Instantiate(proiettiliPrefab[(int)tipoArma.firepump], mira.transform.position, direzioniSparo[i].rotation);
                proiettil.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * proiettil.GetComponent<proiettile>().velocità, ForceMode2D.Impulse);
                if (!nemico)
                {
                    proiettil.GetComponent<proiettile>().proprietario = pm;
                }
                else
                {
                    proiettil.GetComponent<proiettile>().proprietario = transform.parent.GetComponent<playerManager>();
                }
                //a.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 15, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(_secondi);
        }

        staSparando = false;
    }
}
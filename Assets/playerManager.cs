using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public int nCubi, nBarre;
    public Vita vitaSc;
    public arma armaSc;
    public inputManager imSc;
    public movimento movSc;
    public UIPlayer UIPSc;

    void Start()
    {
        vitaSc = GetComponent<Vita>();
        armaSc = GetComponent<arma>();
        imSc = GetComponent<inputManager>();
        movSc = GetComponent<movimento>();
        UIPSc = GetComponent<UIPlayer>();
    }

    public void AcquistaBarraPerCubo()
    {
        if (nCubi > 0)
        {
            nCubi--;
            nBarre++;
            UIPSc.AggiornaValori(UIPSc.barreCont, nBarre);
            UIPSc.AggiornaValori(UIPSc.cubiCont, nCubi);
        }
    }
}
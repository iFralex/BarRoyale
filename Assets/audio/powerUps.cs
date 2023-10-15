using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUps : MonoBehaviour
{
    public enum tipoPowerUp
    {
        speed,
        strongBar,
        damage,
    }
    public playerManager pm;
    public tipoPowerUp tipo;
    public int costo, percInPiù;
    public UnityEngine.UI.Text costoT;

    void Start()
    {
        
    }

    public void AcquistaPowerUp()
    {
        //if (pm.nCubi > 0)
        //{
        costo = System.Convert.ToInt32(costoT.text);
            switch (tipo)
            {
                case tipoPowerUp.speed:
                pm.movSc.velocità += pm.movSc.velocità / 100 * percInPiù;
                    pm.nCubi -= costo;
                    break;
                case tipoPowerUp.strongBar:
                    pm.armaSc.vitaBarra += pm.armaSc.vitaBarra / 100 * percInPiù;
                    pm.nCubi -= costo;
                    break;
                case tipoPowerUp.damage:
                    pm.armaSc.potenza += pm.armaSc.potenza / 100 * percInPiù;
                    pm.nCubi -= costo;
                    break;
            }
        costoT.text = ((int)(costo + ((float)costo / 8))).ToString();
        //}
    }
}
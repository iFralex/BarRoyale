using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mappaManager : MonoBehaviour
{
    public UnityEngine.UI.Image miniMappa;
    Vector2 startPosMiMa, startDimMiMa;

    void Start()
    {
        startPosMiMa = miniMappa.rectTransform.position;
        startDimMiMa = miniMappa.rectTransform.sizeDelta;
    }

    void Update()
    {
        
    }

    public void TuttoSchermoMiniMappa()
    {
        if (!miniMappa.transform.parent.GetComponent<UnityEngine.UI.Image>().enabled)
        {
            miniMappa.rectTransform.position = new Vector2(Screen.width / 2, Screen.height / 2);
            miniMappa.rectTransform.sizeDelta = new Vector2(Screen.height, Screen.height);
            miniMappa.transform.parent.GetComponent<UnityEngine.UI.Image>().enabled = true;
            miniMappa.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().enabled = false;
        }
        else
        {
            miniMappa.rectTransform.position = startPosMiMa;
            miniMappa.rectTransform.sizeDelta = startDimMiMa;
            miniMappa.transform.parent.GetComponent<UnityEngine.UI.Image>().enabled = false;
            miniMappa.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().enabled = true;
        }
    }
}

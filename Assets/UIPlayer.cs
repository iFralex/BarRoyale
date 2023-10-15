using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    public Text barreCont, cubiCont;
    public RectTransform hpBarra, shieldBarra;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AggiornaBarre(RectTransform rectT, float num)
    {
        rectT.localScale = new Vector3(num, rectT.localScale.y, rectT.localScale.z);
    }

    public void AggiornaValori(Text testo, int num)
    {
        testo.text = num.ToString();
    }
}

using UnityEngine;

public class cubo : MonoBehaviour
{
    int i;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (i == 0)
        {
            if (col.GetComponent<playerManager>())
            {
                i++;
                playerManager pm = col.GetComponent<playerManager>();
                pm.nCubi++;
                pm.UIPSc.AggiornaValori(pm.UIPSc.cubiCont, pm.nCubi);
                Destroy(gameObject);
            }
        }
    }
}

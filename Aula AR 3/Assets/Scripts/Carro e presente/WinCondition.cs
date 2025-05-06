using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject pnlVitoria;
    public GameObject pnlDerrota;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pnlVitoria = GameObject.Find("PnlVitoria");
        pnlDerrota = GameObject.Find("PnlDerrota");

        pnlVitoria.SetActive(false);
        pnlDerrota.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

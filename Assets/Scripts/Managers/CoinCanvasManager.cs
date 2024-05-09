using System.Collections;
using System.Collections.Generic;
using Nightmare;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class CoinCanvasManager : MonoBehaviour
{
    public Text coinText;

    // Start is called before the first frame update
    void Start()
    {
        CurrStateData.SetCurrentCoin(5000);
        CurrStateData.InitCurrentPets();
        coinText.text = CurrStateData.GetCurrentCoin().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = CurrStateData.GetCurrentCoin().ToString();
    }
}

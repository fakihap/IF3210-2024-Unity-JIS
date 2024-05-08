using System.Collections;
using System.Collections.Generic;
using Nightmare;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvasManager : MonoBehaviour
{
    public ShopItemObj[] shopItemObjs;
    public GameObject[] shopPanelsGO;
    public ShopItemTemplate[] shopPanels;
    public Button[] buyButtons;
    public GameObject player;
    public GameObject healerPet;
    public GameObject attackerPet;
    public Text coinText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        /* DEBUG */
        CurrStateData.SetCurrentCoin(5000);
        CurrStateData.InitCurrentPets();
        coinText.text = CurrStateData.GetCurrentCoin().ToString();
        Debug.Log("Current coin (state data): " + CurrStateData.GetCurrentCoin().ToString());
        /*TO DO: get current player coint*/
        shopPanels = new ShopItemTemplate[shopItemObjs.Length];
        for (int i = 0; i < shopItemObjs.Length; i++)
        {
            var shopPanelGO = shopPanelsGO[i];
            shopPanelGO.gameObject.SetActive(true);
            shopPanels[i] = shopPanelGO.GetComponent<ShopItemTemplate>();
        }
        LoadPanels();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            // PauseManager.StaticPauseOrUnPause();
            // PauseManager.Pause();
        }   
    }

    public void LoadPanels()
    {
        for(int i = 0; i < shopItemObjs.Length; i++)
        {
            var shopPanel = shopPanels[i];
            var shopItemSO = shopItemObjs[i];
            Debug.Log("Shop Panel: " + shopPanel);
            Debug.Log("Shop Item SO: " + shopItemSO);
            Debug.Log("Shop Item title: " + shopItemSO.title);
            Debug.Log("Shop Item price: " + shopItemSO.price);
            shopPanel.title.text = shopItemSO.title;
            shopPanel.price.text = shopItemSO.price.ToString();
            shopPanel.image.sprite = shopItemSO.image;
            Debug.Log("Shop Panel: " + shopPanel);
            Debug.Log("Shop Panel title: " + shopPanel.title.text);
            Debug.Log("Shop Panel price: " + shopPanel.price.text);
            shopPanel.manager = this;
        }
    }

    public void BroadcastIsPurchasable()
    {
        /*TO DO: get current player coint*/
        coinText.text = CurrStateData.GetCurrentCoin().ToString();
        Debug.Log("Current coin (state data): " + CurrStateData.GetCurrentCoin().ToString());
        for (int i = 0; i < shopItemObjs.Length; i++)
        {
            var shopPanel = shopPanels[i];
            shopPanel.IsPurchasable();
        }
    }
}
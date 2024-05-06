using System.Collections;
using System.Collections.Generic;
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
        /*TO DO: get current player coint*/

        for (int i = 0; i < shopItemObjs.Length; i++)
        {
            var shopPanelGO = shopPanelsGO[i];
            shopPanelGO.gameObject.SetActive(true);
        }
        LoadPanels();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            PauseManager.StaticPauseOrUnPause();
        }   
    }

    public void LoadPanels()
    {
        for(int i = 0; i < shopItemObjs.Length; i++)
        {
            var shopPanel = shopPanels[i];
            var shopItemSO = shopItemObjs[i];
            shopPanel.title.text = shopItemSO.title;
            shopPanel.price.text = shopItemSO.price.ToString();
            shopPanel.manager = this;
        }
    }

    public void BroadcastIsPurchasable()
    {
        /*TO DO: get current player coint*/

        for (int i = 0; i < shopItemObjs.Length; i++)
        {
            var shopPanel = shopPanels[i];
            shopPanel.isPurchasable();
        }
    }
}
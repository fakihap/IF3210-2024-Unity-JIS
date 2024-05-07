using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemTemplate : MonoBehaviour
{
    public Text title;
    public Text price;
    public Image image;
    public Button buyButton;
    public ShopCanvasManager manager;

    // Start is called before the first frame update
    void Start()
    {
        buyButton = GetComponentInChildren<Button>();
        buyButton.onClick.AddListener(BuyItem);
        IsPurchasable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuyItem()
    {
        /* TO DO: substract coin*/
        CurrStateData.SubstractCoin(int.Parse(price.text));
        Debug.Log("Buy");

        int petId=2;
        if(title.text == "Attacker")
        {
            petId = 0;
        }
        else if(title.text == "Healer")
        {
            petId = 1;
        }
        /*TO DO: Add pet*/
        CurrStateData.AddPet(petId);
        Debug.Log($"Length = {CurrStateData.GetPetsLength()}");
        if (CurrStateData.GetPetsLength() == 1)
        {
            PetManager.isSpawnNewPet = true;
        }

        manager.BroadcastIsPurchasable();
    }

    public void IsPurchasable()
    {
        var itemPrice = int.Parse(price.text);
        /*TO DO: check harga dan coin yang dimiliki*/
        if(CurrStateData.GetCurrentCoin() < itemPrice)
        {
            buyButton.gameObject.SetActive(false);
        }
    }
}
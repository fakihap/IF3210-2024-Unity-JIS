using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemTemplate : MonoBehaviour
{
    public Text title;
    public Text description;
    public Text price;
    public Image image;
    public Button buyButton;
    public ShopCanvasManager manager;

    // Start is called before the first frame update
    void Start()
    {
        buyButton = GetComponentInChildren<Button>();
        buyButton.onClick.AddListener(buyItem);
        isPurchasable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void buyItem()
    {
        /* TO DO: substract coin*/
        Debug.Log("Buy");

        int petType=0;
        if(title.text == "Attacker")
        {
            petType = 1;
        }
        else if(title.text == "Healer")
        {
            petType = 2;
        }
        /*TO DO: Add pet*/

        manager.BroadcastIsPurchasable();
    }

    public void isPurchasable()
    {
        var itemPrice = int.Parse(price.text);
        /*TO DO: check harga dan coin yang dimiliki*/
    }
}
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopBuilding;
    public GameObject shopBuildingClosed;
    private Animator _anim;
    private ShopBuildingEffect shopBuildingEffect;
    private ShopBuildingClosedEffect shopBuildingClosedEffect;
    public GameObject shopCanvas;
    // public GameObject enemyManager;
    private float startTime;
    public float timeLimit;
    private static readonly int BuyClick = Animator.StringToHash("BuyClick");
    private static readonly int BuyErrorClick = Animator.StringToHash("BuyErrorClick");

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        shopBuildingEffect = shopBuilding.GetComponent<ShopBuildingEffect>();
        shopBuildingClosedEffect = shopBuildingClosed.GetComponent<ShopBuildingClosedEffect>();
        shopCanvas.SetActive(false);
        startTime = Time.time;
        PauseManager.StaticPauseOrUnPause();
        shopBuilding.SetActive(true);
        shopBuildingClosed.SetActive(false);
        // enemyManager.SetActive(false);
    }

    // Update is called once per frame
    // Update is called once per frame
void Update()
{
    // Check if the shop has closed
    if ((Time.time - startTime) > (timeLimit + 5))
    {
        shopBuilding.SetActive(false);
        shopBuildingClosed.SetActive(true);
    }

    // Update shop effects components
    shopBuildingEffect = shopBuilding.GetComponent<ShopBuildingEffect>();
    shopBuildingClosedEffect = shopBuildingClosed.GetComponent<ShopBuildingClosedEffect>();

    // Debug logs to check player range detection
    //Debug.Log("Is Player in open shop range: " + shopBuildingEffect.isPlayerInShopRange);
    //Debug.Log("Is Player in closed shop range: " + shopBuildingClosedEffect.isPlayerInShopRange);

    // Check if the open shop is active
    if (shopBuilding.activeSelf)
    {
        if (shopBuildingEffect.isPlayerInShopRange)
        {
            // Player is in the range of the open shop
            _anim.SetBool("ShopClosed", false);
            _anim.SetBool("IsPlayerInRange", true);
        }
        else
        {
            _anim.SetBool("IsPlayerInRange", false);
        }
    }
    // Check if the closed shop is active
    else if (shopBuildingClosed.activeSelf)
    {
        if (shopBuildingClosedEffect != null && shopBuildingClosedEffect.isPlayerInShopRange)
        {
            // Player is in the range of the closed shop
            _anim.SetBool("ShopClosed", true);
            _anim.SetBool("IsPlayerInRange", true);
        }
        else
        {
            _anim.SetBool("IsPlayerInRange", false);
        }
    }

    // Handle player input
    if (Input.GetKey(KeyCode.B) && !PauseManager.IsPaused())
    {
        PauseManager.IsPaused();
        Debug.Log("Press B");
        if (!shopBuildingEffect.isPlayerInShopRange && shopBuilding.activeSelf)
        {
            // Player is not in the range of the open shop
            Debug.Log("False");
            _anim.SetTrigger(BuyErrorClick);
        }
        else if(shopBuildingClosed.activeSelf)
        {
            _anim.SetBool("ShopClosed", true);
            _anim.SetBool("IsPlayerInRange", true);
        }
        else
        {
            // Player is in the range of the open shop
            PauseManager.Pause();
            shopCanvas.SetActive(true);
        }
    }
}

public void openShop() {
    startTime = Time.time;
    shopBuilding.SetActive(true);
    shopBuildingClosed.SetActive(false);
}

}
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopBuilding;
    private Animator _anim;
    private ShopBuildingEffect shopBuildingEffect;
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
        shopCanvas.SetActive(false);
        startTime = Time.time;
        PauseManager.StaticPauseOrUnPause();
        shopBuilding.SetActive(true);
        // enemyManager.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!shopBuilding.activeSelf)
        {
            return;
        }

        if ((Time.time - startTime) > (timeLimit + 5))
        {
            shopBuilding.SetActive(false);
            // enemyManager.SetActive(true);
            
            return;
        }

        if (Time.time - startTime > timeLimit)
        {
            shopBuildingEffect.isPlayerInShopRange = true;
            return;
        }

        if (shopBuildingEffect.isPlayerInShopRange)
        {
            Debug.Log("Masuk Range");
            _anim.SetBool("IsPlayerInRange", true);
        }
        else
        {
            _anim.SetBool("IsPlayerInRange", false);
        }

        if (Input.GetKey(KeyCode.B) && !PauseManager.IsPaused())
        {
            PauseManager.IsPaused();
            Debug.Log("Press B");
            if (!shopBuildingEffect.isPlayerInShopRange)
            {
                Debug.Log("False");
                _anim.SetTrigger(BuyErrorClick);
            }
            else
            {
                // PauseManager.StaticPauseOrUnPause();
                PauseManager.Pause();
                // PauseManager.healthCanvas.enabled = false;
                // PauseManager.pauseCanvas.enabled = false;                    
                shopCanvas.SetActive(true);
            }
        }
    }
}
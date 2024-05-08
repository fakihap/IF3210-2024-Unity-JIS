using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PetHealthBarManager : MonoBehaviour
{
    public Slider petAttackerBar;
    public Slider petHealerBar;
    // Start is called before the first frame update
    void Start()
    {
        CurrStateData.InitCurrentPets();
        petAttackerBar.gameObject.SetActive(false);
        petHealerBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrStateData.GetCurrentPet()==0)
        {
            petAttackerBar.gameObject.SetActive(true);
        }
        else if(CurrStateData.GetCurrentPet()==1)
        {
            petHealerBar.gameObject.SetActive(true);
        }
        else{
            petAttackerBar.gameObject.SetActive(false);
            petHealerBar.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    CutsceneManager cutsceneManager;
    void Awake()
    {
        cutsceneManager = GameObject.FindObjectOfType<CutsceneManager>();
    }

    void Update()
    {
        
    }

     public void OnPointerDown(PointerEventData eventData)
    {
        cutsceneManager.ProgressCutscene();
    }
}

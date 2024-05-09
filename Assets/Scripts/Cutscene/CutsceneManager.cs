using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    [Header("UI Elements")]
    [SerializeField]
    private Image bgImage;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI dialogueText;


    [Header("Dialogues")]
    [SerializeField]
    private int cutsceneDialoguesIndex = -1;             // index for which cutscene
    [SerializeField]
    private List<Cutscene> cutscenes;

    [SerializeField]
    private bool isInCutscene = false;

    void Start()
    {
        mainCamera = Camera.main;
        DisableUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isInCutscene) {
            StartCutscene();
        }
    }

    void StartCutscene() {
        if (NextCutscene()) {
            isInCutscene = true;
            EnableUI();

            ProgressCutscene();
            
            return;
        }

        // check this behavior later
        EndCutscene();
    }

    public void ProgressCutscene() {
        // if cutscene is finished
        if (!CurrentCutscene().NextDialogue()) {
            isInCutscene = false;
            DisableUI();
            return;
        }

        Dialogue currentDialogue = CurrentCutscene().GetCurrentDialogue();

        nameText.text = currentDialogue.name;
        dialogueText.text = currentDialogue.text;
        mainCamera.transform.position = currentDialogue.camTransform.position;
        mainCamera.transform.rotation = Quaternion.Euler(currentDialogue.camTransform.rotation);
    }

    void EndCutscene() {
        // currently using build index, might cause progress corruption
        // load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    Cutscene CurrentCutscene() {
        return cutscenes[cutsceneDialoguesIndex];
    }

    bool NextCutscene() {
        if (cutsceneDialoguesIndex + 1 >= cutscenes.Count) {
            return false;
        }

        cutsceneDialoguesIndex += 1;
        return true;
    }

    void EnableUI() {
        bgImage.enabled = true;

        nameText.enabled = true;
        dialogueText.enabled = true;
    }

    void DisableUI() {
        bgImage.enabled = false;

        nameText.enabled = false;
        dialogueText.enabled = false;
    }
}
 
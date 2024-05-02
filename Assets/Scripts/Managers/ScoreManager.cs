using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace Nightmare
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;
        // private int levelThreshhold;
        // const int LEVEL_INCREASE = 300;

        public TextMeshProUGUI text;

        void Awake ()
        {
            text = FindObjectOfType<TextMeshProUGUI> ();
            // sText = GetComponent <TMPro.TextMeshProUGUI> ().text;

            score = 0;
            // levelThreshhold = LEVEL_INCREASE;
        }


        void Update ()
        {
            text.text = "Score: " + score;
            // if (score >= levelThreshhold)
            // {
            //     AdvanceLevel();
            // }
        }

        // private void AdvanceLevel()
        // {
        //     levelThreshhold = score + LEVEL_INCREASE;
        //     LevelManager lm = FindObjectOfType<LevelManager>();
        //     lm.AdvanceLevel();
        // }
    }
}
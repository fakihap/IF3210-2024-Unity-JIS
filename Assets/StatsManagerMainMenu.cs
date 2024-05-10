using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsManagerMainMenu : MonoBehaviour
{
    public TextMeshProUGUI accuracyVal;
    public TextMeshProUGUI distanceTravelledVal;
    public TextMeshProUGUI playedtimeVal;
    public TextMeshProUGUI damageDealtVal;
    public TextMeshProUGUI damageTakenVal;
    public TextMeshProUGUI enemyKilledVal;

    // Start is called before the first frame update
    public void UpdateStats()
    {                
        int shotCount = PlayerPrefs.GetInt("shotCount");
        int hitCount = PlayerPrefs.GetInt("hitCount");

        print("count :" + shotCount + " hits :" + hitCount);
        
        if (shotCount != 0) {
            float accuracy = (float)hitCount / (float)shotCount;
            accuracyVal.text = ((int) (accuracy * 100)).ToString() + "%";
        } else {
            accuracyVal.text =  "0%";
        }

        print("count :" + shotCount + " hits :" + hitCount);

        distanceTravelledVal.text = ((int)PlayerPrefs.GetFloat("distanceTravelled")).ToString() + "m";

        float time = PlayerPrefs.GetFloat("elapsedTime");
        playedtimeVal.text = ((int) time / 60).ToString() + " minutes";

        damageDealtVal.text = PlayerPrefs.GetInt("damageDealt").ToString();
        damageTakenVal.text = PlayerPrefs.GetInt("damageTaken").ToString();
        enemyKilledVal.text = PlayerPrefs.GetInt("enemyKilled").ToString();
    }
    void Start()
    {
        UpdateStats();
    }
}
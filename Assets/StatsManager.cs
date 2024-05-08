using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public TextMeshProUGUI accuracyVal;
    public TextMeshProUGUI distanceTravelledVal;
    public TextMeshProUGUI playedtimeVal;
    public TextMeshProUGUI damageDealtVal;
    public TextMeshProUGUI damageTakenVal;
    public TextMeshProUGUI enemyKilledVal;

    public void UpdateStats()
    {
        accuracyVal.text = (Convert.ToInt32(CurrStateData.GetShotAccuracy() * 100)).ToString() + "%";
        distanceTravelledVal.text = (Convert.ToInt32(CurrStateData.distanceTravelled)).ToString() + "m";
        playedtimeVal.text = CurrStateData.getMinutesPlaying().ToString() + " Minutes";
        damageDealtVal.text = CurrStateData.damageDealt.ToString();
        damageTakenVal.text = CurrStateData.damageTaken.ToString();
        enemyKilledVal.text = CurrStateData.enemyKilled.ToString();
    }
    void Start()
    {
        accuracyVal.text = (Convert.ToInt32(CurrStateData.GetShotAccuracy() * 100)).ToString() + "%";
        distanceTravelledVal.text = (Convert.ToInt32(CurrStateData.distanceTravelled)).ToString() + "m";
        playedtimeVal.text = CurrStateData.getMinutesPlaying().ToString() + "Minutes";
        damageDealtVal.text = CurrStateData.damageDealt.ToString();
        damageTakenVal.text = CurrStateData.damageTaken.ToString();
        enemyKilledVal.text = CurrStateData.enemyKilled.ToString();

    }

}

using System;
using UnityEngine;

public class TravelQuestNotifier : QuestNotifier
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            // target reached
            Notify();
        }
    }
}
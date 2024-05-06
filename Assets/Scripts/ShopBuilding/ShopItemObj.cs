using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

[CreateAssetMenu(fileName="shopMenu", menuName="Scriptable Objects/New Shop Item", order=1)]
public class ShopItemObj : ScriptableObject
{
    public string type;
    public string title;
    public Sprite image;
    public int price;
}
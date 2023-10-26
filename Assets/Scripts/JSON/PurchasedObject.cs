using System;
using System.Collections.Generic;

[Serializable]
public class PurchasedObject 
{
    public int Coins = 0;
    public int BestScore = 0;
    public int BoughtIndex = 0;
    public List<string> PurchasedItems = new List<string>();
}
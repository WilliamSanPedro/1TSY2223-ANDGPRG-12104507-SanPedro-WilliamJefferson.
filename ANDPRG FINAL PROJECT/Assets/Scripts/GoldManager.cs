using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
public class GoldManager : MonoBehaviour
{
    public TMP_Text goldText; 
    public int gold = 0;
    public int goldMult = 1;
    public GameObject soldierToSpawn;
    public Transform soldierSpawnPoint;
    public GameObject minerToSpawn;
    public Transform minerSpawnPoint;
    void Start()
    {
        goldText.text = "Gold: " + gold.ToString();
        InvokeRepeating("IncreaseGold", 1f, goldMult);
    }

    void IncreaseGold()
    {
        gold += goldMult;
        goldText.text = "Gold: " + gold.ToString();
    }

    public void BuySoldier()
    {
        if (gold >= 25)
        {
            gold -= 25;

            Instantiate(soldierToSpawn, soldierSpawnPoint.position, Quaternion.identity);
            
        }

    }
    public void BuyMiner()
    {
        if (gold >= 50)
        {
            gold -= 50;

            Instantiate(minerToSpawn, minerSpawnPoint.position, Quaternion.identity);
            goldMult += 1;
        }

    }
}
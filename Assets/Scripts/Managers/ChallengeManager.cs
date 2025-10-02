using System;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    [Header("Scriptable Objects")]
    public DroneDataSO droneData;
    public InputDataSO inputData;
    public UIDataSO uiData;

    [Header("Local Objects")]
    public List<GameObject> AllCoins;
    public int coinCount = 0;
    public float time = 0;


    private void OnEnable()
    {
        TurnOnCoins();
        coinCount = 0;
        time = 0;
        droneData.DroneTouchedEvent += CoinCollision;
        inputData.SetInputActivated(true);
    }

    private void OnDisable()
    {
        TurnOffCoins();
        droneData.DroneTouchedEvent -= CoinCollision;
        inputData.SetInputActivated(false);
    }


    private void Update()
    {
        if (droneData.isStarted)
        {
            time += Time.deltaTime;
            
        }
    }

    private void CoinCollision(GameObject taggedObject)
    {
        if (taggedObject.CompareTag("Coin") && coinCount < AllCoins.Count)
        {
            AllCoins[coinCount].SetActive(false);
            coinCount++;
        }

        if (taggedObject.CompareTag("Table") && taggedObject.name == "EndTable")
        {
            // Challenge completed
            inputData.SetInputActivated(false);
            droneData.isStarted = false;
            uiData.coinCount = coinCount;
            uiData.totalTime = time;
            uiData.EndChallenge();
            Debug.Log("Challenge Completed!");
        }

    }

    private void TurnOnCoins()
    {
        foreach (var coin in AllCoins)
        {
            coin.SetActive(true);
        }
    }

    private void TurnOffCoins()
    {
        foreach (var coin in AllCoins)
        {
            coin.SetActive(false);
        }
    }
}

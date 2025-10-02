using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header(" Scriptable Objects ")]
    public UIDataSO uiData;
    public DroneDataSO droneData;

    [Header(" Managers ")]
    public GameObject tutorialaManager;
    public GameObject practiseManager;
    public GameObject challengeManager;


    private void OnEnable()
    {
        uiData.StartTutorialEvent += StartTutorial;
        uiData.StartPracticeEvent += StartPractise;
        uiData.StartChallengeEvent += StartChallenge;

        uiData.HomeEvent += ResetManagers;

    }


    private void OnDisable()
    {
        uiData.StartTutorialEvent -= StartTutorial;
        uiData.StartPracticeEvent -= StartPractise;
        uiData.StartChallengeEvent -= StartChallenge;

        uiData.HomeEvent -= ResetManagers;
    }

    private void StartTutorial()
    {
        ResetManagers();
        tutorialaManager.SetActive(true);
    }

    private void StartPractise()
    {
        ResetManagers();
        practiseManager.SetActive(true);
    }

    private void StartChallenge()
    {
        ResetManagers();
        challengeManager.SetActive(true);
    }

    private void ResetManagers()
    {
        tutorialaManager.SetActive(false);
        practiseManager.SetActive(false);
        challengeManager.SetActive(false);
    }
}

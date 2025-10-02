using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UIDataSO", menuName = "ScriptableObjects/UIDataSO", order = 1)]
public class UIDataSO : ScriptableObject
{

    public int coinCount = 0;
    public float totalTime = 0;


    // Action
    public Action StartTutorialEvent;
    public Action EndTutorialEvent;

    public Action StartPracticeEvent;
    public Action EndPracticeEvent;

    public Action StartChallengeEvent;
    public Action EndChallengeEvent;

    public Action HomeEvent;

    //Methods

    public void StartTutorial()
    {
        StartTutorialEvent?.Invoke();
    }

    public void EndTutorial()
    {
        EndTutorialEvent?.Invoke();
    }

    public void StartPractice()
    {
        StartPracticeEvent?.Invoke();
    }

    public void EndPractice()
    {
        EndPracticeEvent?.Invoke();
    }

    public void StartChallenge()
    {
        StartChallengeEvent?.Invoke();
    }

    public void EndChallenge()
    {
        EndChallengeEvent?.Invoke();
    }

    public void Home()
    {
        HomeEvent?.Invoke();
    }

}

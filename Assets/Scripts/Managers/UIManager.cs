using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Scriptable Objects")]
    public InputDataSO inputData;
    public UIDataSO uiData;

    [Header("UI Panels")]
    public GameObject MainCanvas;
    public GameObject MainPanel;
    public GameObject ExitPanel;
    public GameObject TutorialPanel;
    public GameObject ChallengePanel;

    private void OnEnable()
    {
        inputData.MenuBtnClickedEvent += ShowExitPopUp;
        uiData.EndChallengeEvent += ShowChallengePanel;
        uiData.HomeEvent += GoToHome;
       
    }

    private void OnDisable()
    {
        inputData.MenuBtnClickedEvent -= ShowExitPopUp;
        uiData.EndChallengeEvent -= ShowChallengePanel;
        uiData.HomeEvent -= GoToHome;
        
    }

    private void ShowChallengePanel()
    {
        ResetPanels();
        MainCanvas.SetActive(true);
        ChallengePanel.SetActive(true);
    }

    public void GoToHome()
    {
        ResetPanels();
        MainCanvas.SetActive(true);
        MainPanel.SetActive(true);
    }

    private void ShowExitPopUp()
    {
        ResetPanels();
        MainCanvas.SetActive(true);
        ExitPanel.SetActive(true);
    }

    private void ResetPanels()
    {    
        MainPanel.SetActive(false);
        ExitPanel.SetActive(false);
        TutorialPanel.SetActive(false);
        ChallengePanel.SetActive(false);
    }
}

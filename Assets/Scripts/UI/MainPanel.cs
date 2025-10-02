using System.Collections;
using TMPro;
using UnityEngine;

public class MainPanel : MonoBehaviour
{
    [Header("Scriptable Objects")]
    public AudioDataSO audioData;
    public UIDataSO uiData;

    [Header("UI Elements")]
    public GameObject MainCanvas;
    public TMP_Text debugText;


    // local variables
    private Coroutine displayRoutine;



    public void TutorialBtnClicked()
    {
        audioData.PlayBtnClickSound();
        uiData.StartTutorial();
        
    }

    public void PractiseBtnClicked()
    {
        audioData.PlayBtnClickSound();
        uiData.StartPractice();
        MainCanvas.SetActive(false);
    }

    public void ChallengeBtnClicked()
    {
        audioData.PlayBtnClickSound();
        uiData.StartChallenge();
        MainCanvas.SetActive(false);
    }


  
}

using TMPro;
using UnityEngine;

public class ChallengePanel : MonoBehaviour
{
    [Header("Scriptable Objects")]
    public AudioDataSO audioData;
    public UIDataSO uiData;


    [Header(" UI Elements")]
    public GameObject MainCanvas;
    public TMP_Text coinText;
    public TMP_Text timeText;


    private void OnEnable()
    {
        coinText.text = "Coins: " + uiData.coinCount + "/10";
        timeText.text = "Time: " + uiData.totalTime.ToString("F2") + "s";
    }

  
    public void HomeBtnClicked()
    {
        audioData.PlayBtnClickSound();
        uiData.Home();
    }

    public void ReplayBtnClicked()
    {
        audioData.PlayBtnClickSound();
        uiData.Home();
        uiData.StartChallenge();
        MainCanvas.SetActive(false);
    }

}

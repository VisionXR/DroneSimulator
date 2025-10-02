using UnityEngine;

public class ExitPanel : MonoBehaviour
{
    [Header("Scriptable Objects")]
    public AudioDataSO audioData;
    public UIDataSO uiData;


    public void YesBtnClicked()
    {
        audioData.PlayBtnClickSound();
        uiData.Home();

    }

    public void NoBtnClicked()
    {
        audioData.PlayBtnClickSound();
        gameObject.SetActive(false);
    }
}

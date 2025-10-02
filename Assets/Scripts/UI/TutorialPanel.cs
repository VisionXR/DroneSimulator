using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class TutorialPanel : MonoBehaviour
{
    [Header(" Scriptable Objects")]
    public TutorialDataSO tutorialData;


    [Header("UI for turtorial")]
    public TMP_Text StepText;
    public TMP_Text ContentText;
    public TMP_Text SuccessFailureText;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public GameObject NextButton;
    public GameObject PlayButton;
    public GameObject SkipButton;



    public void Reset()
    {
        PlayButton.SetActive(false);
        NextButton.SetActive(false);
        ContentText.text = "";
        SuccessFailureText.text = "";

    }
}

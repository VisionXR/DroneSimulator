using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Scriptable Objects")]
    public AudioDataSO audioData;
    public TutorialDataSO tutorialData;
    public TutorialStep[] tutorialSteps;


    [Header("References")]
    public TutorialPanel tutorialPanel;
    public int currentStepIndex = 0;
    public TutorialStep currentStep;


    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }


    public void StartTutorial()
    {

    }


    public void EndTutorial()
    {

    }

    private void DisplayCurrentStep()
    {
        // ... your existing code to display the step ...

        currentStep = tutorialSteps[currentStepIndex];
        tutorialPanel.StepText.text = "Step : " + currentStep.stepNumber + " / 11";
        tutorialPanel.ContentText.text = currentStep.stepText;

        if (currentStep.stepAudio != null)
        {
            tutorialPanel.audioSource.clip = currentStep.stepAudio;
            tutorialPanel.audioSource.Play();
        }

        if (currentStep.stepVideo != null)
        {

            tutorialPanel.videoPlayer.clip = currentStep.stepVideo;
            tutorialPanel.videoPlayer.Play();
        }

        if (currentStep.interactiveStepType == InteractiveStepType.Normal)
        {
            StartCoroutine(WaitForStepToComplete(5));
        }
        else
        {
            
            switch (currentStep.interactiveStepType)
            {
                case InteractiveStepType.Power:
                    SetUpPower();
                    break;
                case InteractiveStepType.Throttle:
                    SetUpThrottle();
                    break;
                case InteractiveStepType.Yaw:
                    SetUpYaw();
                    break;
            }
        }
    }

    private IEnumerator WaitForStepToComplete(float t)
    {

        yield return new WaitForSeconds(t);
        tutorialPanel.SuccessFailureText.text = "";
        tutorialPanel.NextButton.SetActive(true);

        if (currentStepIndex == tutorialSteps.Length - 1)
        {
            tutorialPanel.NextButton.SetActive(false);
            tutorialPanel.PlayButton.SetActive(true);
        }
    }

    public void NextStep()
    {
        audioData.PlayBtnClickSound();
        if (currentStepIndex < tutorialSteps.Length - 1)
        {
            tutorialPanel.NextButton.SetActive(false);
            currentStepIndex++;
            DisplayCurrentStep();
        }
        else if (currentStepIndex == tutorialSteps.Length - 1)
        {
            tutorialPanel.NextButton.SetActive(false);
            tutorialPanel.PlayButton.SetActive(true);
        }
    }

    private void SetUpPower()
    {

    }

    private void SetUpThrottle()
    {

    }

    private void SetUpYaw()
    {

    }
}

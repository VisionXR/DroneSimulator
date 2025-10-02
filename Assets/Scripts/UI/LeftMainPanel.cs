using System.Collections;
using TMPro;
using UnityEngine;

public class LeftMainPanel : MonoBehaviour
{
    [Header("Scriptable Objects")]
    public AudioDataSO audioData;
    public UIDataSO uiData;
    public DroneDataSO droneData;

    [Header("UI Elements")]
    public TMP_Text crashText;


    // local variables
    private Coroutine displayRoutine;


    private void OnEnable()
    {
        droneData.DroneCollidedEvent += ShowCrashText;
    }

    private void OnDisable()
    {
        droneData.DroneCollidedEvent += ShowCrashText;
    }

    private void ShowCrashText()
    {
        if(displayRoutine != null)
        {
            StopCoroutine(displayRoutine);
        }

        displayRoutine = StartCoroutine(DisplayText("Drone Crashed!"));
    }

    private IEnumerator DisplayText(string text)
    {
        crashText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        crashText.gameObject.SetActive(false);

        displayRoutine = null;
    }
}

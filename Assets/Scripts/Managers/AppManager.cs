using System.Collections;
using UnityEngine;

public class AppManager : MonoBehaviour
{

    [Header(" Scriptable Objects ")]
    public AppDataSO appData;


    // local variables
    private bool isLeft, isRight;

    public void PlayHapticVibration()
    {
        StartCoroutine(PlayHapticVibrationCoroutine());
    }

    // Summary: Start haptic vibration for a given duration
    public IEnumerator PlayHapticVibrationCoroutine()
    {
        if (isRight)
        {
            OVRInput.SetControllerVibration(appData.vibrationAmplitude, appData.vibrationAmplitude, OVRInput.Controller.RTouch);
        }
        else if (isLeft)
        {
            OVRInput.SetControllerVibration(appData.vibrationAmplitude, appData.vibrationAmplitude, OVRInput.Controller.LTouch);
        }

        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startTime + appData.vibrationDuration)
        {
            yield return null;
        }

        StopVibration();
    }

    public void StopVibration()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
    }


    public void LeftHandHovered()
    {

        isLeft = true;
    }

    public void RightHandHovered()
    {

        isRight = true;
    }

    public void LeftHandUnHovered()
    {
        isLeft = false;
    }

    public void RightHandUnUnHovered()
    {
        isRight = false;
    }
}

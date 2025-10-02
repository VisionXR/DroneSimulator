using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AppDataSO", menuName = "ScriptableObjects/AppDataSO", order = 1)]
public class AppDataSO : ScriptableObject
{
    [Header(" Colors ")]
    public Color SelectedColor;
    public Color HoverColor;
    public Color IdleColor;
    public Color HoverIdle;


    [Header(" Local variables")]
    public float vibrationDuration = 0.1f;
    public float vibrationAmplitude = 0.1f;

    // Actions
    public Action PlayHapticVibrationEvent;
    public Action StopHapticVibrationEvent;

    //Methods
    public void PlayHapticVibration()
    {
        PlayHapticVibrationEvent?.Invoke();
    }

    public void StopHapticVibration()
    {
        StopHapticVibrationEvent?.Invoke();
    }

}

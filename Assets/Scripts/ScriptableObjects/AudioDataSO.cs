using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioDataSO", menuName = "ScriptableObjects/AudioDataSO", order = 1)]
public class AudioDataSO : ScriptableObject
{



    // Actions
    public Action PlayBtnClickSoundEvent;


    // Methods
    public void PlayBtnClickSound()
    {
        PlayBtnClickSoundEvent?.Invoke();
    }
}

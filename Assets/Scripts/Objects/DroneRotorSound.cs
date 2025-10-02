using UnityEngine;

/// <summary>
/// Controls rotor sound pitch/volume based on throttle.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class DroneRotorSound : MonoBehaviour
{
    [Header("SO")]
    public DroneDataSO droneData;
    public AudioSource rotorAudio;

    [Header("Sound Settings")]
    public float minPitch = 0.5f;
    public float maxPitch = 2.0f;
    public float minVolume = 0f;
    public float maxVolume = 1.0f;


    private void OnEnable()
    {
        rotorAudio.Play();
    }

    private void OnDisable()
    {
        rotorAudio.Stop();
    }



    private void Update()
    {


        if (droneData.isStarted)
        {
            // Get throttle input from your DroneController (0..1)
            float throttle = Normalize(droneData.currentThrottle,-1,1);

            // Smooth interpolation
            rotorAudio.pitch = Mathf.Lerp(minPitch, maxPitch, throttle);
            rotorAudio.volume = Mathf.Lerp(minVolume, maxVolume, throttle);
        }
        else
        {

            rotorAudio.volume = 0;
        }

    }

    private float Normalize(float value, float min, float max)
    {
        return (value - min) / (max - min);
    }
}

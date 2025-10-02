using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DroneData", menuName = "ScriptableObjects/DroneDataSO", order = 1)]
public class DroneDataSO : ScriptableObject
{
    [Header("Drone Properties")]
    public string droneName;
    public float mass;
    public float radius;
    public float minAngularSpeed;
    public float maxAngularSpeed;
    public float maxThrustForce;
    
    // Add other parameters as needed, for example:
    public float batteryCapacity; // in mAh
    public float maxFlightTime;   // in seconds


    [Header("Roll Settings")]
    public float maxRollAngle = 15f;     // degrees
    public float maxPitchAngle = 15f;      // thrust bias factor

    public float rollStrength = 0.75f;      // thrust bias factor
    public float pitchStrength = 0.75f;      // thrust bias factor
    public float yawStrength = 0.75f;      // thrust bias factor
    public float horizontalControlGain = 5f; // roll/pitch command -> torque (N*m) (optional)

    public float yawControlGain = 0.2f;  // direct yaw command -> torque (N*m) (optional)
    public float rotorTorqueCoef = 0.02f;// reaction torque per Newton of thrust (N*m per N) - tune
    public int[] spinDir = new int[4] { +1, -1, +1, -1 }; // FR,FL,BL,BR


    public float autoLevelStrength = 0.5f; // how strongly to auto-level when no input


    [Header(" Drone variables")]
    public bool isStarted = false;
    public float currentThrottle;          // input 0..1
    public float currentRollInput;         // input -1..1
    public float currentPitchInput;        // input -1..1
    public float currentYawInput;          // input -1..1


    // Actions
    public Action<float> ApplyThrottleEvent;
    public Action<Vector2> ApplyRollAndPitchEvent;
    public Action<float,float,float,float> ApplyThrustRollPitchAndYawEvent;
    public Action<bool,bool> CamBtnClickiedEvent;
    public Action<bool> PowerButtonClickedEvent;
    public Action DroneCollidedEvent;
    public Action<GameObject> DroneTouchedEvent;



    // Methods

    private void OnEnable()
    {
        isStarted = false;
        currentThrottle = 0f;
        currentRollInput = 0f;
        currentPitchInput = 0f;
        currentYawInput = 0f;
    }
    public void ApplyThrottle(float throttle)
    {
        ApplyThrottleEvent?.Invoke(throttle);
    }

    public void ApplyRollAndPitch(Vector2 rollAndPitch)
    {
        ApplyRollAndPitchEvent?.Invoke(rollAndPitch);
    }

    public void ApplyThrustRollPitchAndYaw(float throttle, float roll, float pitch, float yaw)
    {
        ApplyThrustRollPitchAndYawEvent?.Invoke(throttle, roll, pitch, yaw);
    }

    public void ToggleCam(bool fpvStatus,bool altitudeStatus)
    {
        CamBtnClickiedEvent?.Invoke(fpvStatus,altitudeStatus);
    }

    public void TogglePower(bool power)
    {
        PowerButtonClickedEvent?.Invoke(power);
    }

    public void DroneCollided()
    {
        DroneCollidedEvent?.Invoke();
    }

    public void DroneTouched(GameObject tag)
    {
        DroneTouchedEvent?.Invoke(tag);
    }
}

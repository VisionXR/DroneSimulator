using System;
using UnityEngine;

/// <summary>
/// Realistic quadcopter controller using per-rotor thrust mixing.
/// Inputs: throttle [0..1], roll [-1..1], pitch [-1..1], yaw [-1..1].
/// Hover occurs at throttle=0.5.
/// Roll/pitch limited by max angles. 
/// Thrust applied per rotor with AddForceAtPosition.
/// </summary>
public class DroneController : MonoBehaviour
{
    [Header("Drone Settings")]
    public DroneDataSO droneData;          // contains mass, strengths, max angles etc.
    public Rigidbody rb;
    public Transform propFR;               // Front Right
    public Transform propFL;               // Front Left
    public Transform propBL;               // Back Left
    public Transform propBR;               // Back Right



    [Header("Curves")]
    public AnimationCurve throttleCurve = new AnimationCurve();

    // Add this field at class scope
    private float modeBlend = 0f; // 0 = rotor mode, 1 = demo mode
    [SerializeField] private float blendSpeed = 5f; // how fast to transition between modes

    // local variables
    public float offsetWeight = 0.01f;
    public float hoverForce;              // N required to hover
    

    private void OnEnable()
    {
        droneData.PowerButtonClickedEvent += PowerBtnClicked;
        droneData.ApplyThrustRollPitchAndYawEvent += Mix;
        droneData.DroneCollidedEvent += TurnOffPower;
    }

    private void OnDisable()
    {
        droneData.PowerButtonClickedEvent -= PowerBtnClicked;
        droneData.ApplyThrustRollPitchAndYawEvent -= Mix;
        droneData.DroneCollidedEvent -= TurnOffPower;
    }

    public void TurnOffPower()
    {
        droneData.isStarted = false;
    }

    private void PowerBtnClicked(bool state)
    {

        if (state)
        {
            droneData.isStarted = !droneData.isStarted;

        }
    }

    private void Start()
    {
        hoverForce = (droneData.mass * Physics.gravity.magnitude) - offsetWeight;
        rb.mass = droneData.mass;
    }


    private void FixedUpdate()
    {
        if (droneData.isStarted)
        {
          //  ApplyMixer(droneData.currentThrottle, droneData.currentRollInput, droneData.currentPitchInput, droneData.currentYawInput);
            ApplyMixer(droneData.currentThrottle, droneData.currentRollInput, droneData.currentPitchInput, droneData.currentYawInput);
        }
    }

    public void Mix(float throttle, float roll, float pitch, float yaw)
    {

        if (droneData.isStarted)
        {
            droneData.currentThrottle = Mathf.Clamp(MapThrottle(throttle), -1, 1);
            droneData.currentRollInput = Mathf.Clamp(roll, -1f, 1f);
            droneData.currentPitchInput = Mathf.Clamp(pitch, -1f, 1f);
            droneData.currentYawInput =  Mathf.Clamp(MapYaw(yaw), -1f, 1f);

        }
        else
        {
            droneData.currentThrottle = 0f;
            droneData.currentRollInput = 0f;
            droneData.currentPitchInput = 0f;
            droneData.currentYawInput = 0f;

        }
    }

    /// <summary>
    /// Mixes throttle + roll + pitch + yaw into per-rotor thrusts.
    /// </summary>
    private void ApplyMixer(float throttle, float roll, float pitch, float yaw)
    {
        
        // --- Base hover thrust ---
        float totalThrust = hoverForce + throttle * hoverForce;   // throttle=0 ? hover
        float baseThrust = totalThrust / 4f;

        // --- Clamp roll/pitch angles ---
        // Inside ApplyMixer
        float rollAngle = GetRollAngle();
        float pitchAngle = GetPitchAngle();


        float rollCorrection = 0;
        if (roll == 0)
        {
            rollCorrection = -(rollAngle / droneData.maxRollAngle) * droneData.autoLevelStrength;
        }

        float rollCommand = roll + rollCorrection;
        float rollOffset =  rollCommand * droneData.rollStrength;



        float pitchCorrection = 0;
        if (pitch == 0)
        {
            pitchCorrection = -(pitchAngle / droneData.maxPitchAngle) * droneData.autoLevelStrength;
        }

        float pitchCommand = pitch + pitchCorrection;
        float pitchOffset =  pitchCommand * droneData.pitchStrength;

        // 1. Current tilt
        float pitchRad = Mathf.Deg2Rad * pitchAngle;
        float rollRad = Mathf.Deg2Rad * rollAngle;


        if ((roll > 0 && rollAngle >= droneData.maxRollAngle) ||
           (roll < 0 && rollAngle <= -droneData.maxRollAngle))
        {
            rollOffset = 0f;
         

        }
    

        if ((pitch > 0 && pitchAngle >= droneData.maxPitchAngle) ||
            (pitch < 0 && pitchAngle <= -droneData.maxPitchAngle))
        {
            pitchOffset = 0;
          

        }


        if (pitchAngle != 0)
        {
            
            baseThrust /= Mathf.Cos(pitchRad);
        }


        // --- Mix corrections into each rotor ---
        // FR = base - roll + pitch - yaw
        float thrustFR = baseThrust - rollOffset - pitchOffset;

        // FL = base + roll + pitch + yaw
        float thrustFL = baseThrust + rollOffset - pitchOffset ;

        // BL = base + roll - pitch - yaw
        float thrustBL = baseThrust + rollOffset + pitchOffset ;

        // BR = base - roll - pitch + yaw
        float thrustBR = baseThrust - rollOffset + pitchOffset ;

        // --- Apply forces ---
        ApplyRotorForce(propFR, thrustFR);
        ApplyRotorForce(propFL, thrustFL);
        ApplyRotorForce(propBL, thrustBL);
        ApplyRotorForce(propBR, thrustBR);


        float yawOffset = yaw * droneData.yawStrength;

        // --- Compute yaw torque from rotor reaction (spinDir * rotorTorqueCoef * thrust) ---
        // Reaction torque is approximated proportional to thrust for a simple sim
        float yawFromRotorDrag =
            droneData.spinDir[0] * droneData.rotorTorqueCoef * thrustFR +
            droneData.spinDir[1] * droneData.rotorTorqueCoef * thrustFL +
            droneData.spinDir[2] * droneData.rotorTorqueCoef * thrustBL +
            droneData.spinDir[3] * droneData.rotorTorqueCoef * thrustBR;

        // --- Add pilot yaw command (optional direct torque) to help responsiveness ---
        float yawCommandTorque = yaw * droneData.yawControlGain;

        float totalYawTorque = yawFromRotorDrag + yawCommandTorque;

        // Apply yaw torque around world-up in body space
        rb.AddTorque(transform.up * totalYawTorque, ForceMode.Force);
    }

   


    /// <summary>
    /// Applies upward thrust force at a rotor's position.
    /// </summary>
    private void ApplyRotorForce(Transform prop, float thrust)
    {
        Vector3 force = prop.up * Mathf.Max(0, thrust); // prevent negative thrust
        rb.AddForceAtPosition(force, prop.position, ForceMode.Force);
    }

    // --- Angle helpers ---
    private float GetRollAngle()
    {
        return -Vector3.SignedAngle(Vector3.up, rb.transform.up, rb.transform.forward);
    }

    private float GetPitchAngle()
    {
        return Vector3.SignedAngle(Vector3.up, rb.transform.up, rb.transform.right);
    }

    private float MapThrottle(float rawInput)
    {

        if (rawInput < 0.1 && rawInput > -0.1)
        {
            return 0;
        }
        else
        {
            return RoundOff(throttleCurve.Evaluate(rawInput));
        }
    }

    private float MapYaw(float rawInput)
    {
        if (rawInput < 0.4 && rawInput > -0.4)
        {
            return 0;
        }
        else
        {
            return RoundOff(throttleCurve.Evaluate(rawInput));
        }
    }

    private float RoundOff(float value)
    {
        return Mathf.Round(value * 100f) / 100f;
    }
}

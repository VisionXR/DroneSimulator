using UnityEngine;

public class DroneRotorAnimation : MonoBehaviour
{
    [Header("References")]
    public DroneDataSO droneData;
    public Transform propellerObj; // Reference to the propeller object


    [Header("Rotor Settings")]
    public float maxRPM = 2000f; // Maximum rotations per minute
    public float spinDirection = 1f; // 1 for clockwise, -1 for counter-clockwise

    void Update()
    {
        if (propellerObj == null)
            return;

        // Get throttle value (assumed to be 0 to 1)
        float throttle = Normalize(droneData.currentThrottle,-1,1);

        if (throttle > 0f && droneData.isStarted)
        {
            // Calculate rotation speed based on throttle
            float currentRPM = Mathf.Lerp(0f, maxRPM, throttle);
            float degreesPerSecond = currentRPM * 6f; // 360 degrees * RPM / 60

            // Rotate the propeller around its local Z axis
            propellerObj.Rotate(Vector3.up, degreesPerSecond * Time.deltaTime*spinDirection, Space.Self);

            //if(propellerObj.transform.eulerAngles.y > 360)
            //{
            //    propropellerObj.transform.eulerAngles.y -= 360
            //}
        }
    }

    private float Normalize(float value, float min, float max)
    {
        return (value - min) / (max - min);
    }
}

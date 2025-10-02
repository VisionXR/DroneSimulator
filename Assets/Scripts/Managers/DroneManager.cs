using System;
using System.Collections;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    [Header("Drone Data")]
    public UIDataSO uiData;
    public InputDataSO inputData;
    public DroneDataSO droneData;

    [Header(" Current Drone Objects")]
    public GameObject currentDrone;
    public DroneController currentDroneController;




    public GameObject initialDronePosition;

    private void OnEnable()
    {
        droneData.DroneCollidedEvent += ResetDrone;
        uiData.HomeEvent += Home;
    }

    private void OnDisable()
    {
        droneData.DroneCollidedEvent -= ResetDrone;
        uiData.HomeEvent -= Home;
    }

    private void Home()
    {
        inputData.SetInputActivated(false);
        currentDroneController.TurnOffPower();
        StartCoroutine(WaitAndCrash(0));
    }

    private void ResetDrone()
    {
       StartCoroutine(WaitAndCrash(3));
    }

    private IEnumerator WaitAndCrash(float time)
    {
        Rigidbody rb = currentDrone.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        yield return new WaitForSeconds(time);
        if (currentDrone != null && initialDronePosition != null)
        {
            currentDrone.transform.position = initialDronePosition.transform.position;
            currentDrone.transform.rotation = initialDronePosition.transform.rotation;
           
        }
    }
}

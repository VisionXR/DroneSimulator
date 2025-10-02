using System;
using UnityEngine;

public class DroneCamera : MonoBehaviour
{
    [Header("Scriptable Objects")]
    public DroneDataSO droneData;
    public UIDataSO uiData;

    [Header("Camera Feeds")]
    public GameObject fpvCamFeed;
    public GameObject altitudeCamFeed;
   


    private void OnEnable()
    {
        droneData.CamBtnClickiedEvent += ToggleCam;
        uiData.HomeEvent += ResetPanels;
    }

    private void OnDisable()
    {
        droneData.CamBtnClickiedEvent -= ToggleCam;
        uiData.HomeEvent += ResetPanels;
    }

    private void ResetPanels()
    {
        fpvCamFeed.SetActive(false);
        altitudeCamFeed.SetActive(false);
    }

    private void ToggleCam(bool fpvStatus,bool altitudeStatus)
    {
        if (fpvStatus)
        {
            fpvCamFeed.SetActive(true);
            altitudeCamFeed.SetActive(false);
        }
        else if(altitudeStatus)
        {
            fpvCamFeed.SetActive(false);
            altitudeCamFeed.SetActive(true);
        }
    }
}

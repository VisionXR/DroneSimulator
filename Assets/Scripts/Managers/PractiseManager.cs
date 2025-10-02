using UnityEngine;

public class PractiseManager : MonoBehaviour
{
    [Header("Sciptable Objects")]
    public InputDataSO inputData;


    private void OnEnable()
    {
        inputData.SetInputActivated(true);
    }

    private void OnDisable()
    {
        inputData.SetInputActivated(false);
    }
}

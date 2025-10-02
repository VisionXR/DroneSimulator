using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InputDataSO", menuName = "ScriptableObjects/InputDataSO", order = 1)]
public class InputDataSO : ScriptableObject
{
     public bool isInputActivated = false;


    // Actions
    public Action MenuBtnClickedEvent;

    private void OnEnable()
    {
        isInputActivated = false;
    }


    public void MenuBtnClicked()
    {
        MenuBtnClickedEvent?.Invoke();
    }


    public bool IsInputActivated()
    {
        return isInputActivated;
    }

    public void SetInputActivated(bool isActivated)
    {
        isInputActivated = isActivated;
    }
}

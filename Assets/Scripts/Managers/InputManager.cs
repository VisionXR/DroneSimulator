using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Central manager for reading XR, Gamepad, and Keyboard input
/// through Unity's new Input System. 
/// Provides unified access to movement and throttle.
/// </summary>
public class InputManager : MonoBehaviour
{
    [Header("Input Actions Asset & SO")]
    public InputActionAsset inputActions;
    public DroneDataSO droneData;
    public InputDataSO inputData;


    [Header("Input Actions")]
    private InputAction throttleAndYawAction;
    private InputAction rollAndPitchAction;
    private  InputAction fpcCamBtnAction;
    private  InputAction altitudeCamBtnAction;
    private InputAction powerBtnAction;
    private InputAction menuBtnAction;

    [Header("Local Variables")]
    private Vector2 ThrottleAndYaw;
    private Vector2 RoleAndPitch;
    private bool isFPVCamButtonClicked;
    private bool isAltitudeCamButtonClicked;
    private bool isPowerButtonClicked;
    private bool isMenuButtonClicked;



    private void OnEnable()
    {
        if (inputActions == null)
        {
            Debug.LogError("InputActions asset not assigned!");
            return;
        }

        // Find the action directly by name (no null maps)
        throttleAndYawAction = inputActions.FindAction("ThrottleAndYaw", throwIfNotFound: false);
        rollAndPitchAction = inputActions.FindAction("RollAndPitch", throwIfNotFound: false);
      
        fpcCamBtnAction = inputActions.FindAction("FpvCamBtn", throwIfNotFound: false);
        altitudeCamBtnAction = inputActions.FindAction("AltitudeCamBtn", throwIfNotFound: false);
        powerBtnAction = inputActions.FindAction("PowerBtn", throwIfNotFound: false);
        menuBtnAction = inputActions.FindAction("MenuBtn", throwIfNotFound: false);

        if (throttleAndYawAction != null && rollAndPitchAction!= null)
        {
            throttleAndYawAction.Enable();
            rollAndPitchAction.Enable();
            fpcCamBtnAction.Enable();
            altitudeCamBtnAction.Enable();
            powerBtnAction.Enable();

            menuBtnAction.Enable();

        }
        else
        {
            Debug.LogError("Throttle action not found in InputActions!");
        }
    }

    private void OnDisable()
    {
        if (throttleAndYawAction != null && rollAndPitchAction != null)
        {
            throttleAndYawAction.Disable();
            rollAndPitchAction.Disable();
            fpcCamBtnAction.Disable();
            altitudeCamBtnAction.Disable();
            powerBtnAction.Disable();
            menuBtnAction.Disable();

        }
    }

    private void LateUpdate()
    {

        if (inputData.isInputActivated)
        {
            isFPVCamButtonClicked = fpcCamBtnAction.WasPerformedThisFrame();
            isAltitudeCamButtonClicked = altitudeCamBtnAction.WasPerformedThisFrame();
            isMenuButtonClicked = menuBtnAction.WasPerformedThisFrame();

            if (isMenuButtonClicked)
            {
               inputData.MenuBtnClicked();
            }

            droneData.ToggleCam(isFPVCamButtonClicked,isAltitudeCamButtonClicked);

            isPowerButtonClicked = powerBtnAction.WasPerformedThisFrame();
            droneData.TogglePower(isPowerButtonClicked);

            // Read movement vector (works for XR, Gamepad, Keyboard)
            ThrottleAndYaw = throttleAndYawAction.ReadValue<Vector2>();
            RoleAndPitch = rollAndPitchAction.ReadValue<Vector2>();
        }


    }

    private void FixedUpdate()
    {
           
         droneData.ApplyThrustRollPitchAndYaw(ThrottleAndYaw.y, RoleAndPitch.x, RoleAndPitch.y, ThrottleAndYaw.x);      
    }


}

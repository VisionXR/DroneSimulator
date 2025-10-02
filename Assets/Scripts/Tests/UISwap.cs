using Oculus.Interaction;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class UISwap : MonoBehaviour
{
    public Canvas mainCanvas;
    public InputSystemUIInputModule standaloneInputModule;
    public PointableCanvasModule canvasModule;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Application.isEditor)
        {
            mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            standaloneInputModule.enabled = true;
            canvasModule.enabled = false;
        }
    }


}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonStates : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    [Header(" Scriptable Objects ")]
    public AppDataSO appData;

    [Header(" Button Images ")]
    public Image BackgroundImage;
    public Image HoverImage;
    public Image ToolTipImage;

    private bool isHovering = false;


    public void OnPointerEnter(PointerEventData eventData)
    {

        if (BackgroundImage.gameObject.GetComponent<UIGradient>().enabled == false)
        {
            isHovering = true;
            HoverImage.color = appData.HoverColor;
            appData.PlayHapticVibration();

        }
        // Perform desired actions when the pointer enters the button

        if (ToolTipImage != null)
        {
            ToolTipImage.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       // Debug.Log(" exit id is " + eventData.);
        if (BackgroundImage.gameObject.GetComponent<UIGradient>().enabled == false)
        {
            isHovering = false;
            HoverImage.color = appData.HoverIdle;
           
        }
        // Perform desired actions when the pointer exits the button

        if (ToolTipImage != null)
        {
            ToolTipImage.gameObject.SetActive(false);
        }

        appData.StopHapticVibration();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isHovering)
        {
            Debug.Log("Pointer clicked");
            HoverImage.color = appData.HoverIdle;
            // Perform desired actions when the pointer clicks the button
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       
    }

    public void OnDrag(PointerEventData eventData)
    {
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
    }
}

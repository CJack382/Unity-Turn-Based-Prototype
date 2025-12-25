using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler 
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private Vector2 originalLocalPointerPosition; //Mouse Pointer position
    private Vector3 originalPanelLocalPosition; //original position of card
    private Vector3 originalScale; //original scale of card

    private int currentState = 0;

    private Quaternion originalRotation;

    private Vector3 originalPosition;

    [SerializeField] private float selectScale = 1.1f; //Slightly increases card scale when hovering

    [SerializeField] private Vector2 cardPlay;
    [SerializeField] private Vector3 playPosition;

    [SerializeField] private GameObject glowEffect; //Highlight image
    [SerializeField] private GameObject playArrow;

    [SerializeField] private float lerpFactor = 0.1f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;

    }

    void Update()
    {
        switch (currentState)
        {
            case 1:
                HandleHoverState();
                break;
            case 2:
                HandleDragState();
                if (Mouse.current.leftButton.ReadValue() == 0) //If releasing the click button (***NEW INPUT SYSTEM NOT THE SAME AS VID***)
                {
                    TransitionToState0();
                }
                break;
            case 3:
                HandlePlayState();
                if (Mouse.current.leftButton.ReadValue() == 0)
                {
                    TransitionToState0();
                }
                break;
        }
    }

    private void TransitionToState0()
    {
        currentState = 0;
        rectTransform.localPosition = originalPosition;
        rectTransform.localRotation = originalRotation;
        rectTransform.localScale = originalScale;

        glowEffect.SetActive(false);
        playArrow.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData) //OnPointerEnter checks if the mouse is beginning to hover over an object, in this case it sets the state to 1, activating HandleHoverState()
    { //Also to note, PointerEventData is merely the information that the cursor is sending to Unity
        if (currentState == 0) //Making currentState 0 in transitiontostate0 allows us to ensure that no overlap happens where HandleHoverState() is called before the card has been reset to its standard parameters
        {
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            originalScale = rectTransform.localScale;

            currentState = 1; 
        }
    }

    public void OnPointerDown(PointerEventData eventData) //OnPointerDown Checks if the player has clicked or begun to hold down the Mouse 1 button, in this case it enters state 2 and allows you to drag a card
    {
        if (currentState == 1)
        {
            currentState = 2;
            //Finds a specific point in the rectangle (first variable), using the PointerEventData (second variable), and the camera (If Screen Space overlay, it's null, in this case the screen space scales w/screen size, so you
            //take the camera data from PointerEventData). The final variable is the specific point you wish to find
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
            originalPanelLocalPosition = rectTransform.localPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentState == 2) 
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPointerPosition))
            {
                rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, lerpFactor);

                if(Input.mousePosition.y > cardPlay.y) //Whenever card is dragged above predetermined y position
                {
                    currentState = 3;
                    playArrow.SetActive(true);
                    rectTransform.localPosition = Vector3.Lerp(rectTransform.position, playPosition, lerpFactor);
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData) //OnPointerExit occurs when the mouse cursor is no longer hovering over an object, so in this case it checks if you stop hovering over a card when in state 1, then returns it to 0
    {
        if (currentState == 1)
        {
            TransitionToState0();
        }
    }

    private void HandleHoverState() //When hovering over a card, highlights and slight increases the size of the card
    {
        glowEffect.SetActive(true);
        rectTransform.localScale = originalScale * selectScale;
    }

    private void HandleDragState()
    {
        //Set card rotation to 0
        rectTransform.localRotation = Quaternion.identity;
    }

    private void HandlePlayState()
    {
        rectTransform.localPosition = playPosition;
        rectTransform.localRotation = Quaternion.identity;
        
        if (Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2;
            playArrow.SetActive(false);
        }
    }
}

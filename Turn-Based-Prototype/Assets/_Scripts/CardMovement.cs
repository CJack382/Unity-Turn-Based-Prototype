using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler 
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private RectTransform canvasRectTransform;

    private Vector3 originalScale; //original scale of card

    private int currentState = 0;

    private Quaternion originalRotation;

    private Vector3 originalPosition;

    private GridManager gridManager;

    [SerializeField] private float selectScale = 1.1f; //Slightly increases card scale when hovering

    [SerializeField] private Vector2 cardPlay;
    [SerializeField] private Vector3 playPosition;

    [SerializeField] private GameObject glowEffect; //Highlight image
    [SerializeField] private GameObject playArrow;

    [SerializeField] private float lerpFactor = 0.1f;

    [SerializeField] private int cardPlayDivider = 4;
    [SerializeField] private float cardPlayMultiplier = 1f;

    //[SerializeField] private bool needUpdateCardPlayPosition = false;

    [SerializeField] private int playPositionYDivider = 2;
    [SerializeField] private float playPositionYMultiplier = 1f;
    [SerializeField] private int playPositionXDivider = 4;
    [SerializeField] private float playPositionXMultiplier = 2f;

    //[SerializeField] private bool needUpdatePlayPosition = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas != null )
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }

        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;

        UpdateCardPlayPosition();
        UpdatePlayPosition();
        gridManager = FindAnyObjectByType<GridManager>();
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
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentState == 2) 
        {
            if(Input.mousePosition.y > cardPlay.y) //Whenever card is dragged above predetermined y position
            {
                currentState = 3;
                playArrow.SetActive(true);
                rectTransform.localPosition = Vector3.Lerp(rectTransform.position, playPosition, lerpFactor);
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
        rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, lerpFactor);
    }

    private void HandlePlayState()
    {
        rectTransform.localPosition = playPosition;
        rectTransform.localRotation = Quaternion.identity;
        
        if (Mouse.current.leftButton.ReadValue() == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Uses Raycasting by pinpointing a ray through the camera, in the specified point (In this case where the mouse/cursor is)
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction); //Returns the 2D Raycast by determining the origin of the ray, and the direction the ray is pointing (In this case the origin would be the camera
                                                                             //and the direction would be the camera -> cursor), which determines what the mouse is hitting

            if (hit.collider != null && hit.collider.GetComponent<GridCell>()) //Interestingly, GetComponent<Component>() is a bool statement, neato
            {
                GridCell cell = hit.collider.GetComponent<GridCell>();
                Vector2 targetPos = cell.gridIndex;

                if (gridManager.AddObjectToGrid(GetComponent<CardDisplay>().cardData.prefab, targetPos))
                {
                    HandManager handManager = FindAnyObjectByType<HandManager>();
                    handManager.cardsInHand.Remove(gameObject);
                    handManager.UpdateHandVisuals();
                    Debug.Log("Placed Character");
                    Destroy(gameObject);
                }
            }
            TransitionToState0();
        }

        if (Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2;
            playArrow.SetActive(false);
        }
    }

    private void UpdateCardPlayPosition()
    {
        if (cardPlayDivider != 0 && canvasRectTransform != null)
        {
            float segment = cardPlayMultiplier / cardPlayDivider;

            cardPlay.y = canvasRectTransform.rect.height * segment;
        }
    }

    private void UpdatePlayPosition()
    {
        if (canvasRectTransform != null && playPositionYDivider != 0 && playPositionYMultiplier != 0 && playPositionXDivider != 0 && playPositionXMultiplier != 0)
        {
            float segmentX = playPositionXMultiplier / playPositionXDivider;
            float segmentY = playPositionYMultiplier / playPositionYDivider;

            //Dogshit fix for some dogshit code...
            playPosition.x = canvasRectTransform.rect.width * -segmentX * 1.4f;
            playPosition.y = canvasRectTransform.rect.height * segmentY;
        }
    }
}

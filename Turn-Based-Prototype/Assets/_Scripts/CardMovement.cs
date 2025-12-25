using UnityEngine;
using UnityEngine.EventSystems;

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

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponent<Canvas>();
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
                if (!Input.GetMouseButtonDown(0)) //If player is not pressing 0
                {
                    TransitionToState0();
                }
                break;
            case 3:
                HandlePlayState();
                if (!Input.GetMouseButtonDown(0)) //If player is not pressing 0
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

    }
}

using UnityEngine;

[RequireComponent (typeof(SpriteRenderer), typeof(GridCell))] //Automatically ADDS the designated component as a dependency
public class GridCellDisplay : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color highLightColor = Color.cyan; 
    public Color posColor = Color.green;
    public Color negColor = Color.red;

    private Color originalColor;

    public GameObject[] backgrounds;
    private bool setBackground = false;

    public GridCell gridCell;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        gridCell = GetComponent<GridCell>();
    }

    private void Update()
    {
        if (!setBackground)
        {
            SetBackground();
        }
    }

    private void OnMouseEnter()
    {
        if (!GameManager.Instance.PlayingCard)
        {
            spriteRenderer.color = highLightColor;
        }
        else if (gridCell.cellFull || gridCell.gridIndex.x > 1)
        {
            spriteRenderer.color = negColor;
        }
        else spriteRenderer.color = posColor;
    }

    private void OnMouseExit() 
    {
        spriteRenderer.color = originalColor;
    }

    private void SetBackground()
    {
        if (gridCell.gridIndex.x % 2 == 0)
        {
            backgrounds[0].SetActive(true);
        }
        if (gridCell.gridIndex.x % 2 != 0)
        {
            backgrounds[1].SetActive(true);
        }
        setBackground = true;
    }
}

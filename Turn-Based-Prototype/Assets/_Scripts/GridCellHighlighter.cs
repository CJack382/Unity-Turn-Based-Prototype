using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))] //Automatically ADDS the designated component as a dependency
public class GridCellHighlighter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color highLightColor = Color.yellow;
    private Color originalColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
        spriteRenderer.color = highLightColor;
    }

    private void OnMouseExit() 
    {
        spriteRenderer.color = originalColor;
    }
}

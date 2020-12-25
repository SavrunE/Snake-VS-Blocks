using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    [SerializeField] private Vector2Int destroyPriceRange;
    [SerializeField] private Color[] colors;

    private SpriteRenderer spriteRenderer;
    private int destroyPrice;
    private int filling;

    public int LeftToFill => destroyPrice - filling;
    public int ColorRangeDivision => destroyPriceRange.y / colors.Length;

    public event UnityAction<int> FillingUpdated;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        destroyPrice = Random.Range(destroyPriceRange.x, destroyPriceRange.y);
        FillingUpdated?.Invoke(LeftToFill);
        SetColor();
    }

    public void Fill()
    {
        filling++;
        FillingUpdated?.Invoke(LeftToFill);

        if (filling == destroyPrice)
        {
            Destroy(gameObject);
        }
    }
    private void SetColor()
    {
        int i;
        for (i = 1; LeftToFill > ColorRangeDivision * i; i++)
        {
        }
        spriteRenderer.color = colors[i - 1];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeInput : MonoBehaviour
{
    public Vector2 GetDirectionToClick(Vector2 headPosition)
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - headPosition.x, mousePosition.y - headPosition.y);

        return direction;
    }
}

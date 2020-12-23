using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TailGenerator))]
[RequireComponent(typeof(SnakeInput))]
public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead head;
    [SerializeField] private float speed;
    [SerializeField] private float tailSpringiness;

    private SnakeInput snakeInput;
    private List<Segment> tail;
    private TailGenerator tailGenerator;

    private void Awake()
    {
        tailGenerator = GetComponent<TailGenerator>();
        snakeInput = GetComponent<SnakeInput>();

        tailGenerator.Generate(ref tail);
    }
    private void FixedUpdate()
    {
        Move(head.transform.position + head.transform.up * speed * Time.fixedDeltaTime);
        head.transform.up = snakeInput.GetDirectionToClick(head.transform.position);
    }
    private void Move(Vector3 nextPosition)
    {
        Vector3 previousPosition = head.transform.position;
        foreach (var tailSegment in tail)
        {
            Vector3 tempPosition = tailSegment.transform.position;
            tailSegment.transform.position =
                Vector2.Lerp(tailSegment.transform.position, previousPosition, tailSpringiness * Time.fixedDeltaTime);
            previousPosition = tempPosition;
        }

        head.Move(nextPosition);
    }
}

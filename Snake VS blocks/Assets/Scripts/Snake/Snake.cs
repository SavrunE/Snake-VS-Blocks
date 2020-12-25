using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TailGenerator))]
[RequireComponent(typeof(SnakeInput))]
public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead head;
    [SerializeField] private int tailSize;
    [SerializeField] private float speed;
    [SerializeField] private float tailSpringiness;

    private SnakeInput snakeInput;
    private List<Segment> tail;
    private TailGenerator tailGenerator;

    public event UnityAction<int> SizeUpdated;

    private void Awake()
    {
        tailGenerator = GetComponent<TailGenerator>();
        snakeInput = GetComponent<SnakeInput>();

        tail = new List<Segment>();
        tailGenerator.Generate(ref tail, tailSize);
        SizeUpdated?.Invoke(tail.Count);
    }

    private void Start()
    {
        SizeUpdated?.Invoke(tail.Count);
    }

    private void OnEnable()
    {
        head.BlockCollision += OnBlockCollided;
        head.BonusCollected += OnBonusCollected;
    }

    private void OnDisable()
    {
        head.BlockCollision -= OnBlockCollided;
        head.BonusCollected -= OnBonusCollected;
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

    private void OnBlockCollided()
    {
        Segment deletedSegment = tail[tail.Count - 1];
        tail.Remove(deletedSegment);
        Destroy(deletedSegment.gameObject);

        SizeUpdated?.Invoke(tail.Count);
        if (tail.Count == 0)
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnBonusCollected(int bonusSize)
    {
        tailGenerator.Generate(ref tail, bonusSize);
        SizeUpdated?.Invoke(tail.Count);
    }
}

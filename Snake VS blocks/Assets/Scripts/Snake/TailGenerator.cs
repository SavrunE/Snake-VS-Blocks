using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailGenerator : MonoBehaviour
{
    [SerializeField] private int tailSize;
    [SerializeField] private Segment segmentTemplate;

    public void Generate(ref Stack<Segment> tail)
    {
        tail = new Stack<Segment>();

        for (int i = 0; i < tailSize; i++)
        {
            tail.Push(Instantiate(segmentTemplate, transform));
        }
    }
}

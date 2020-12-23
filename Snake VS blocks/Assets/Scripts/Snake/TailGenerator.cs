using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailGenerator : MonoBehaviour
{
    [SerializeField] private int tailSize;
    [SerializeField] private Segment segmentTemplate;

    public void Generate(ref List<Segment> tail)
    {
        tail = new List<Segment>();

        for (int i = 0; i < tailSize; i++)
        {
            tail.Add(Instantiate(segmentTemplate, transform));
        }
    }
}

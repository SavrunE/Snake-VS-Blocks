using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailGenerator : MonoBehaviour
{
    [SerializeField] private Segment segmentTemplate;

    public void Generate(ref List<Segment> tail, int count)
    {
        for (int i = 0; i < count; i++)
        {
            tail.Add(Instantiate(segmentTemplate, transform));
        }
    }
}

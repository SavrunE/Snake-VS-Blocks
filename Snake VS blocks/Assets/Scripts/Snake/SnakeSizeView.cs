using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Snake))]
public class SnakeSizeView : MonoBehaviour
{
    [SerializeField] private TMP_Text view;

    private Snake snake;

    private void Awake()
    {
        snake = GetComponent<Snake>();
    }
    private void OnEnable()
    {
        snake.SizeUpdated += ChangeView;
    }
    private void OnDisable()
    {
        snake.SizeUpdated -= ChangeView;
    }
    private void ChangeView(int size)
    {
        view.text = size.ToString();
    }
}

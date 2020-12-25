using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeHead : MonoBehaviour
{
    private Rigidbody2D body;

    public event UnityAction BlockCollision;
    public event UnityAction<int> BonusCollected;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector3 newPosition)
    {
        body.MovePosition(newPosition);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Block block))
        {
            block.Fill();
            BlockCollision?.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bonus bonus))
        {
            BonusCollected?.Invoke(bonus.Collect());
        }
    }
}

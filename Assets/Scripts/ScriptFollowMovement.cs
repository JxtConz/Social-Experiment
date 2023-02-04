using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptFollowMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] float _speed;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        LookAtMouse();
        Move();
    }

    private void LookAtMouse()
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = (Vector3)(mousePos - new Vector2(transform.position.x, transform.position.y));
    }

    private void Move()
    {
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), y: Input.GetAxisRaw("Vertical"));

        rb2d.velocity = input.normalized * _speed;
    }
}

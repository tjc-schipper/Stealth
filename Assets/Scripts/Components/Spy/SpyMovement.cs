using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// This must do three things:
/// > Move the spy around with WASD, avoiding walls
/// > Modify movement speed with the crouch key
/// > Face the spy in the direction of the mouse cursor
/// </summary>
public class SpyMovement : Movement
{

    private const float SPEED_STANDING = 4.0f;
    private const float SPEED_CROUCH = 0.75f;

    public SpyPostures post;

    void Start()
    {
        post = GetComponent<SpyPostures>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(horizontalInput, verticalInput, 0f);

        if (post.Covering)
            HandleInput_Cover(input);
        else
            HandleInput(input);
    }

    private void HandleInput(Vector3 input)
    {
        float speed = (post.Crouched) ? SPEED_CROUCH : SPEED_STANDING;
        Vector3 displacement = input * Time.deltaTime * speed;
        Position = Position + displacement;
    }
    private void HandleInput_Cover(Vector3 input)
    {
        throw new NotImplementedException();
    }
}

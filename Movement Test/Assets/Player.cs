using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector3 position;
    private Vector3 direction = Vector3.forward;
    private Vector3 velocity = Vector3.zero;
    private Vector3 acceleration = Vector3.zero;

    float accelerationRate = 2.5f;
    float deccelerationRate = 2f;

    float maxSpeed = 5f;
    float minSpeed = 0.1f;

    float turnSpeed = 3f;

    Vector2 playerInput = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.x > 0)
        {
            TurnBy(-turnSpeed);
        }
        else if (playerInput.x < 0)
        {
            TurnBy(turnSpeed);
        }

        if (playerInput.y > 0)
        {
            acceleration = direction * accelerationRate;

            velocity += acceleration * Time.deltaTime;
        }
        else if (playerInput.y < 0)
        {
            //velocity *= 1 - (deccelerationRate * Time.deltaTime);
            //
            //if (velocity.magnitude < minSpeed)
            //    velocity = Vector3.zero;
            acceleration = direction * accelerationRate;
            velocity -= acceleration * Time.deltaTime;

        }
        else
        {
            velocity *= 1 - (deccelerationRate * Time.deltaTime);
            
            if (velocity.magnitude < minSpeed)
                velocity = Vector3.zero;
        }


        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        position += velocity * Time.deltaTime;

        transform.position = position;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        acceleration = Vector3.zero;
    }


    public void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    public void TurnBy(float turnAngle)
    {
        direction = Quaternion.Euler(0, turnAngle * Time.deltaTime, 0) * direction;
    }
}

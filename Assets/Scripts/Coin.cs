using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed;
    public float floatSpeed;
    public float movementDistance;

    float startingY;
    bool isMovingUp = true;

    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;

        transform.Rotate(transform.up, 360 * Random.Range(0f, 360f));
    }

    void Update()
    {
        Float();
        Spin();
    }

    void Spin()
    {
        transform.Rotate(transform.up, 360 * rotateSpeed * Time.deltaTime);
    }

    void Float()
    {
        float newY = transform.position.y + (isMovingUp ? 1 : -1) * 2 * movementDistance * floatSpeed * Time.deltaTime;

        if (newY > startingY + movementDistance)
        {
            newY = startingY + movementDistance;
            isMovingUp = false;
        }

        else if (newY < startingY)
        {
            isMovingUp = true;
        }

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

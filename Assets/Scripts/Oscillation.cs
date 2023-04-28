using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }
    void Update()
    {
        const float tau = Mathf.PI * 2;
        if(period <= Mathf.Epsilon)
        {
            return;
        }
        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPosition + offset;
    }
}

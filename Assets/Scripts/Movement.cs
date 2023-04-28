using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float thrustMultiplier = 100f;
    [SerializeField] float rotateMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RocketThrust();
        RocketRotate();
    }

    // Thrust rocket forward using space
    void RocketThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrustMultiplier);
        }
        else
        {
            audioSource.Stop();
        }
    }

    // Rotate rocket left or right
    void RocketRotate()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            RotationDirection(-rotateMultiplier);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            RotationDirection(rotateMultiplier);
        }
    }

    void RotationDirection(float rM)
    {
        rb.freezeRotation = false; // stop physics from interfering with manual rotation
        transform.Rotate(Vector3.forward * rM * Time.deltaTime);
        rb.freezeRotation = true; // restart physics once key is not pressed
    }
}

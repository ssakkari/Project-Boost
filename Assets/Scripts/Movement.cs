using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float thrustMultiplier = 100f;
    [SerializeField] float rotateMultiplier = 1f;
    [SerializeField] ParticleSystem mainEngineBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

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
            BoostInput();
        }
        else
        {
            audioSource.Stop();
            mainEngineBooster.Stop();
        }
    }

    // Rotate rocket left or right
    void RocketRotate()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
        }
        else
        {
            rightBooster.Stop();
            leftBooster.Stop();
        }
    }

    // Pressing space to input boost
    void BoostInput()
    {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            if(!mainEngineBooster.isPlaying)
            {
                mainEngineBooster.Play();
            }
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrustMultiplier);
    }

    // Rotate left with left arrow
    void RotateLeft()
    {
            RotationDirection(-rotateMultiplier);
            if(!rightBooster.isPlaying)
            {
                rightBooster.Play();
            }
    }

    // Rotate right with right arrow
    void RotateRight()
    {
            RotationDirection(rotateMultiplier);
            if(!leftBooster.isPlaying)
            {
                leftBooster.Play();
            }
    }

    // Make rotation independent of other objects by disabling their physics on contact
    void RotationDirection(float rM)
    {
        rb.freezeRotation = false; // stop physics from interfering with manual rotation
        transform.Rotate(Vector3.forward * rM * Time.deltaTime);
        rb.freezeRotation = true; // restart physics once key is not pressed
    }
}

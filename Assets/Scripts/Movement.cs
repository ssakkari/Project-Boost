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
    [SerializeField] float indexDelayMultiplier = 1f;

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
        SkipLevel();
        TurnOffCollisions();
    }

    // Disable Collisions
    void TurnOffCollisions()
    {
        if(Input.GetKey(KeyCode.C))
        {
            if (GetComponent<CollisionHandler>().enabled == true)
            {
                GetComponent<CollisionHandler>().enabled = false;
            }
            else
            {
                GetComponent<CollisionHandler>().enabled = true;
            }
        }
    }

    // Skip level
    void SkipLevel()
    {
        if(Input.GetKey(KeyCode.L))
        {
            Invoke ("NextStage", indexDelayMultiplier);
        }
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
            if(!mainEngineBooster.isPlaying)
            {
                mainEngineBooster.Play();
            }
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrustMultiplier);
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
            RotationDirection(-rotateMultiplier);
            if(!rightBooster.isPlaying)
            {
                rightBooster.Play();
            }
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            RotationDirection(rotateMultiplier);
            if(!leftBooster.isPlaying)
            {
                leftBooster.Play();
            }
        }
        else
        {
            rightBooster.Stop();
            leftBooster.Stop();
        }
    }

    void RotationDirection(float rM)
    {
        rb.freezeRotation = false; // stop physics from interfering with manual rotation
        transform.Rotate(Vector3.forward * rM * Time.deltaTime);
        rb.freezeRotation = true; // restart physics once key is not pressed
    }

    void NextStage()
    {
        int nextBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextBuildIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextBuildIndex = 0;
        }
        SceneManager.LoadScene(nextBuildIndex);
    }
}

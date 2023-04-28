using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float indexDelayMultiplier = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    CollisionHandler collisionHandler;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collisionHandler = GetComponent<CollisionHandler>();
    }

    void Update()
    {
        SkipLevel();
        TurnOffCollisions();
    }

    void TurnOffCollisions()
    {
        if(Input.GetKey(KeyCode.C))
        {
            if(collisionHandler.enabled == true)
            {
                collisionHandler.enabled = false;
            }
            else
            {
                collisionHandler.enabled = true;
            }
        }
    }

    // Skip level
    void SkipLevel()
    {
        if(Input.GetKey(KeyCode.L))
        {
            NextStage();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionHandler.enabled == false)
        {
            return;
        }
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
            {
                break;
            }
            case "Finish":
            {
                FinishSequence();
                break;
            }
            default:
            {
                CrashSequence();
                break;
            }
        }
    }

    void FinishSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        if(!successParticles.isPlaying)
        {
            successParticles.Play();
        }
        GetComponent<Movement>().enabled = false;
        Invoke ("NextStage", indexDelayMultiplier);
    }
    void CrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        if(!crashParticles.isPlaying)
        {
            crashParticles.Play();
        }
        GetComponent<Movement>().enabled = false;
        Invoke ("ReloadStage", indexDelayMultiplier);
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
    void ReloadStage()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentBuildIndex);
    }
}
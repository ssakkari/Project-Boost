using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float indexDelayMultiplier = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] int volumeScale = 1;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
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
        audioSource.PlayOneShot(success, volumeScale);
        GetComponent<Movement>().enabled = false;
        Invoke ("NextStage", indexDelayMultiplier);
    }
    void CrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash, volumeScale);
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
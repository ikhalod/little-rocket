using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float nextLevelDelay;

    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip win;

    [SerializeField] ParticleSystem winEffect;
    [SerializeField] ParticleSystem crushEffect;

    AudioSource sound;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S)) 
        {
            ProcessLoadLevel();
        }

        else if (Input.GetKeyUp(KeyCode.D)) 
        {
            collisionDisabled = !collisionDisabled; // toggle collision!
        }
    }

    void RespondToDebugKeys()
    {
        NextLevelEvent();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Respawn":
                Debug.Log("Hey you are alive!");
                break;
            case "Finish":
                NextLevelEvent();
                break;
            default:
                StartCrashEvent();
                break;
        }
    }

    void NextLevelEvent()
    {
        isTransitioning = true;
        sound.Stop();
        sound.PlayOneShot(win);
        winEffect.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ProcessLoadLevel", nextLevelDelay);
    }

    void StartCrashEvent()
    {
        isTransitioning = true;
        sound.Stop();
        sound.PlayOneShot(crash);
        crushEffect.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ProcessReloadLevel", nextLevelDelay);
    }

    void ProcessLoadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextLevelSceneIndex = currentSceneIndex + 1;
        if (NextLevelSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextLevelSceneIndex = 0;
        }
        SceneManager.LoadScene(NextLevelSceneIndex);
    }

    void ProcessReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

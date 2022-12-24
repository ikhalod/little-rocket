using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float NextLevelDelay = 1f;

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Respawn":
                Debug.Log("Hey you are alive!");
                break;
            case "Finish":
                GetComponent<Movement>().enabled = false;
                Invoke("NextLevel", NextLevelDelay);
                break;
            default:
                StartCrashEvent();
                break;
        }
    }

    void StartCrashEvent()
    {
        GetComponent<Movement>().enabled= false;
        Invoke("ReloadLevel", NextLevelDelay);
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextLevelSceneIndex = currentSceneIndex + 1;
        if (NextLevelSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextLevelSceneIndex = 0;
        }
        SceneManager.LoadScene(NextLevelSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

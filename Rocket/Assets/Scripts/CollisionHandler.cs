using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Respawn":
                Debug.Log("Hello there!");
                break;
            case "Finish":
                NextLevel();
                break;
            default:
                ReloadLevel();
                break;
        }
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

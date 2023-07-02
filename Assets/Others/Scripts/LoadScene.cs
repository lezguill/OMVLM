using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public float CinematicTime;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Cinematic1")
        {
            StartCoroutine(LoadNextSceneAfter());
        }
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadNextSceneAfter()
    {
        yield return new WaitForSeconds(14);
        LoadNextScene();
    }
}
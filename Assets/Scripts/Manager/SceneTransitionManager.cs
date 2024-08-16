using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(LoadBattleScene());
        }       
    }
    private IEnumerator LoadBattleScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("WE001BATTLE");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    private IEnumerator LoadWorldScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("WE001");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    public void Run()
    {
        StartCoroutine(LoadWorldScene());
    }
}

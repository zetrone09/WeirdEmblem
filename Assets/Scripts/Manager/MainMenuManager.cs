using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private int _sceneIndex = 1;
    
    public void NewGame()
    {
        SceneManager.LoadSceneAsync(_sceneIndex);
    }
    public void LoadSaveGame()
    {
        SceneManager.LoadSceneAsync(_sceneIndex);
    }
}

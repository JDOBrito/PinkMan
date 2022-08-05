using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private string m_LevelName;

    public void LoadSceneByName(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ReloadScene()
    {
        SoundController.Instance.PlaySound(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  
}

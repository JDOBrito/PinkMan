using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public int totalScore;
    public TMP_Text scoreText;
    public static GameController instance;
    public GameObject m_GameOverScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        instance =this;
    }


    public void UpdateScoreText(){
        scoreText.text= "Score: "+ totalScore.ToString();
    }
    
    public void CallGameOverScreen()
    {
        m_GameOverScreen.SetActive(true);
    }
 
}

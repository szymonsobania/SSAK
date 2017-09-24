using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [SerializeField] private Text playerScoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Text coinsText;

    private const int STARTCOINS = 10;

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.HasKey("Player Score") && PlayerPrefs.HasKey("Best Score"))
        {
            playerScoreText.text = PlayerPrefs.GetInt("Player Score").ToString();
            bestScoreText.text =  PlayerPrefs.GetInt("Best Score").ToString();
        }
        if (PlayerPrefs.HasKey("Coins"))
        {
            coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("Coins", STARTCOINS);
        }
    }
    
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape))
	        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}

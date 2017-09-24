using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    #region SerializeFields
    [SerializeField]
    private float jumpTime = 0.3f;
    [SerializeField]
    private float rotationSpeed = 30;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text coinsText;
    #endregion

    public Vector3 Scale { get; set; }
    public Coroutine coroutine;
    public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }
    public int Coins2Add { get;  set; }
    #region Private Fields   
    private float time = 0;
    private bool jump = false;
    private static int score;
    private static int coinsNumber;
    public float SpeedFactor { get; private set; }

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            coinsNumber = PlayerPrefs.GetInt("Coins");
            coinsText.text = coinsNumber.ToString();
        }
        score = 0;
        Coins2Add = 1;
    }
        private void Update()
    {
        RotatePlayer();
        time += Time.deltaTime;
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Click();            
        }
        SpeedFactor = 1+ (score / 250.0f);
        Scale = new Vector3(0.1f, 0.1f, 0.1f)* transform.localScale.x;
        transform.localScale -= Scale * transform.localScale.x * Time.deltaTime * SpeedFactor;
        if (transform.localScale.x < 0.6)
        {
            GameOver();
        }
    }

    private static void GameOver()
    {
        PlayerPrefs.SetInt("Player Score", score);
        if (!PlayerPrefs.HasKey("Best Score"))
        {
            PlayerPrefs.SetInt("Best Score", score);
        }
        if (score > PlayerPrefs.GetInt("Best Score"))
        {
            PlayerPrefs.SetInt("Best Score", score);
        }
        PlayerPrefs.SetInt("Coins", coinsNumber);
        SceneManager.LoadScene(0);
    }


    public void Click()
    {
    //    if (jump)
    //    {
    //        return;
    //    }
        score++;
        scoreText.text = score.ToString();
        //StartCoroutine(Jump(transform.localScale * 1.35f));
        transform.localScale *= 1.35f;
        transform.GetComponentInChildren<Animator>().Play("Jump");
        //jump = true;      
        coroutine = StartCoroutine(CheckCollison());
    }

    private IEnumerator Jump(Vector3 scale)
    {
        float time = 0;
        while (time < 0.1f)
        {
            time += Time.deltaTime;
            scale -= Scale * transform.localScale.x * Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, scale, time / 0.1f);
            yield return null;
        }        
    }
    #endregion

    public void CollectCoin()
    {
        coinsNumber += Coins2Add;
        coinsText.text = coinsNumber.ToString();
    }

    public void Collision()
    {
        if (coroutine!= null)
        {
            StopCoroutine(coroutine);
        }        
    }
    private IEnumerator CheckCollison()
    {
        yield return new WaitForSeconds(0.1f);
        GameOver();
        //gameover
        jump = false;     
    }

    private void RotatePlayer()
    {
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
    }
}

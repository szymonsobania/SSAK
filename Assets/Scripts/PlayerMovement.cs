using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    #region SerializeFields
    [SerializeField]
    private float jumpTime = 0.3f;
    [SerializeField]
    private Transform targetProvider;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float rotationSpeed =30;
    [SerializeField]
    private Text scoreText;
    #endregion

    public Vector3 Scale { get; set; }
    public Coroutine coroutine;
    public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }
    #region Private Fields   
    private float time = 0;
    private bool jump = false;
    private int score = 0;

    #endregion

    #region Unity Callbacks
   
    private void Update()
    {
        RotatePlayer();
        time += Time.deltaTime;
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Click();            
        }
        Scale = new Vector3(0.1f, 0.1f, 0.1f)* transform.localScale.x;
        transform.localScale -= Scale * transform.localScale.x * Time.deltaTime;
        if (transform.localScale.x < 0.6)
        {
            SceneManager.LoadScene(0);
        }
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
        SceneManager.LoadScene(0);
        //gameover
        jump = false;     
    }

    private void RotatePlayer()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}

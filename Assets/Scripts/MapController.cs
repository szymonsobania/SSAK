using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    #region SerializeFields
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private PlayerMovement player;
    #endregion

  

    #region Private Fields   
    private GameObject[] rings = new GameObject[100];
    private GameObject biggest;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            float scale = 40.0f * Mathf.Pow(1.35f,(i + 1));
            rings[i] = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
            rings[i].transform.localScale = new Vector3(scale, scale, scale);
            rings[i].GetComponentInChildren<Ring>().Player = player.gameObject;
            float dir = 1;
            if (i % 2 == 0) dir = -1;
            rings[i].GetComponentInChildren<Ring>().rotationSpeed = Random.Range(20,40)*dir;
        }
        biggest = rings[9];


    }

    private void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            float rotation = rings[i].GetComponentInChildren<Ring>().rotationSpeed;

            rings[i].transform.Rotate(0, rotation * Time.deltaTime, 0);
            rings[i].transform.localScale -= rings[i].transform.localScale.x * player.Scale * Time.deltaTime;
            if(rings[i].transform.localScale.x <40)
            {
                Destroy(rings[i]);
                rings[i] = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
                rings[i].GetComponentInChildren<Ring>().Player = player.gameObject;
                rings[i].transform.localScale = biggest.transform.localScale * 1.35f;
                float dir = 1;
                if (biggest.GetComponentInChildren<Ring>().rotationSpeed > 0)
                {
                    dir = -1;
                }
                rings[i].GetComponentInChildren<Ring>().rotationSpeed = Random.Range(20, 40) * dir;
                biggest = rings[i];
            }
        }

    }
    #endregion

    #region Public Methods
    
    #endregion
}

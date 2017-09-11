﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    #region SerializeFields
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private PlayerMovement player;
    #endregion

    #region Static
    static int MAP_SIZE = 100;
    static float HEX_RADIUS = 1.0f;
    static float SQRT3 = Mathf.Sqrt(3.0f);
    static float HEX_DIST_FACTOR_X = SQRT3 * HEX_RADIUS;
    static float HEX_DIST_FACTOR_Z = 1.5f * HEX_RADIUS;
    #endregion

    #region Private Fields   
    private GameObject[] rings = new GameObject[100];
    private GameObject biggest;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        //rings = new Ring[100];
        //for (int i = 0; i < 100; i++)
        //{
        //    rings[i] = new Ring();
        //}

        for (int i = 0; i < 10; i++)
        {
            float scale = 40.0f * Mathf.Pow(1.35f,(i + 1));
            rings[i] = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
            rings[i].transform.localScale = new Vector3(scale, scale, scale);
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
            //Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);
            rings[i].transform.localScale -= rings[i].transform.localScale.x * player.Scale * Time.deltaTime;
            if(rings[i].transform.localScale.x <40)
            {
                Destroy(rings[i]);
                rings[i] = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
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
    public static Vector3 CalculateTileCenterPosition(int x, int z)    
    {
        float posX = x * HEX_DIST_FACTOR_X;
        float posZ = z * HEX_DIST_FACTOR_Z;

        if (Mathf.Abs(z % 2) == 1)
        {
            posX += (0.5f * HEX_DIST_FACTOR_X);
        }
        return (new Vector3(posX, 0.0f, posZ));
    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour {


    public float rotationSpeed =5;
    public GameObject ring;

    #region Unity Callbacks
    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<Tile>();
            child.gameObject.AddComponent<MeshCollider>();
            //child.GetComponent<MeshCollider>().isTrigger = true;
        }
    }
    #endregion
}

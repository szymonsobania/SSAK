using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour {


    public float rotationSpeed =5;
    public GameObject ring;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float coinFactor = 0.1f;

    public GameObject Player { get { return player; } set { player = value; } }

    #region Unity Callbacks
    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<Tile>();
            child.gameObject.AddComponent<MeshCollider>();
            
            if (Random.Range(0, 1.0f) < coinFactor)
            {
                var coin = Instantiate(coinPrefab, child);
                child.GetComponent<Tile>().Coin = coin;
            }
            //child.GetComponent<MeshCollider>().isTrigger = true;
        }
    }
    #endregion
}

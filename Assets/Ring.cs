using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Ring : MonoBehaviour {


    public float rotationSpeed =5;
    public GameObject ring;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private List<GameObject> bonusPrefabs;
    [SerializeField] private float coinFactor = 0.1f;
    [SerializeField] private float bonusFrequency = 0.08f;

    public GameObject Player { get { return player; } set { player = value; } }

    #region Unity Callbacks
    private void Start()
    {
        coinFactor = PlayerPrefs.GetFloat("FrequencyFactor");
        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<Tile>();
            child.gameObject.AddComponent<MeshCollider>();
            
            if (Random.Range(0, 1.0f) < coinFactor)
            {
                var coin = Instantiate(coinPrefab, child);
                child.GetComponent<Tile>().Coin = coin;
            }
            else
            {
                foreach (var bonus in bonusPrefabs)
                {
                    if (Random.Range(0, 1.0f) < bonusFrequency)
                    {
                        var newBonus = Instantiate(bonus, child);
                        child.GetComponent<Tile>().Bonus = newBonus.GetComponent<IBonus>();
                        break;
                    }
                }
            }
        }
    }
    #endregion
}

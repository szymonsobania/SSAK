using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    #region SerializeFields
    [SerializeField]
    private List<GameObject> tilePrefabs;
    [SerializeField]
    private PlayerMovement player;

    [SerializeField] private float rotationSpeedMin = 20;
    [SerializeField] private float rotationSpeedMax = 40;
    [SerializeField] private GameObject coinPrefab;
    #endregion



    #region Private Fields   
    private GameObject[] rings = new GameObject[100];
    private List<GameObject> bonusCoins = new List<GameObject>();
    private GameObject biggest;
    
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            float scale = 40.0f * Mathf.Pow(1.35f,(i + 1));
            rings[i] = Instantiate(tilePrefabs[0], Vector3.zero, Quaternion.identity);
            rings[i].transform.localScale = new Vector3(scale, scale, scale);
            rings[i].GetComponentInChildren<Ring>().Player = player.gameObject;
            float dir = 1;
            if (i % 2 == 0) dir = -1;
            rings[i].GetComponentInChildren<Ring>().rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax) *dir;
        }
        biggest = rings[9];


    }

  

    private void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            float rotation = rings[i].GetComponentInChildren<Ring>().rotationSpeed;

            rings[i].transform.Rotate(0, rotation * Time.deltaTime, 0);
            rings[i].transform.localScale -= rings[i].transform.localScale.x * player.Scale * Time.deltaTime *player.SpeedFactor;
            if(rings[i].transform.localScale.x <40)
            {
                Destroy(rings[i]);
                rings[i] = Instantiate(tilePrefabs[Random.Range(0, tilePrefabs.Count)], Vector3.zero, Quaternion.identity);
                rings[i].GetComponentInChildren<Ring>().Player = player.gameObject;
                rings[i].transform.localScale = biggest.transform.localScale * 1.35f;
                float dir = 1;
                if (biggest.GetComponentInChildren<Ring>().rotationSpeed > 0)
                {
                    dir = -1;
                }
                rings[i].GetComponentInChildren<Ring>().rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax) * dir*player.SpeedFactor;
                biggest = rings[i];
            }
        }

    }
    #endregion

    #region Public Methods

    public void ChangeSpeed (float speedFactor)
    {
        rotationSpeedMin *= speedFactor;
        rotationSpeedMax *= speedFactor;
        //foreach (var ring in rings)
        for (int i =0; i<10; i++)
        {
            rings[i].GetComponentInChildren<Ring>().rotationSpeed *= speedFactor;
        }
    }

    public IEnumerator AddBonusCoins()
    {
        for (int i = 0; i < 10; i++)
        {
            var tiles = rings[i].GetComponentsInChildren<Tile>();
            for (int j =0; j< tiles.Length ; j++)
            {
                if (tiles[j].Coin == null)
                {
                    if (tiles[j].Bonus == null)
                    {
                        var coin = Instantiate(coinPrefab, tiles[j].transform);
                        tiles[j].GetComponent<Tile>().Coin = coin;
                        bonusCoins.Add(coin);
                    }
                }
            }
            
        }
        yield return null;
    }

    public void RemoveBonusCoins()
    {
        foreach (var bonusCoin in bonusCoins)
        {
            Destroy(bonusCoin);
        }
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    

    public GameObject Coin { get;  set; }

    private void OnTriggerEnter(Collider other)
    {
        if (Coin != null)
        {
            Coin.SetActive(false);
            var player = GetComponentInParent<Ring>().Player;
            var playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.CollectCoin();
        }
      
    }


}

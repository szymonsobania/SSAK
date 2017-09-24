using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public GameObject Coin { get;  set; }
    public IBonus Bonus { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (Coin != null)
        {
            Coin.SetActive(false);
            var player = GetComponentInParent<Ring>().Player;
            var playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.CollectCoin();
        }
        if (Bonus != null)
        {
            Bonus.StartBonus();
        }
    }
}

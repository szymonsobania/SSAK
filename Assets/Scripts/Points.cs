using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Points : MonoBehaviour, IBonus
    {

        public void StartBonus()
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            StartCoroutine(Bonus());
        }

        private IEnumerator Bonus()
        {
            var player = GetComponentInParent<Ring>().Player;
            var playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.Coins2Add = 2;
            yield return new WaitForSeconds(PlayerPrefs.GetFloat("2xPointsFactor"));
            playerMovement.Coins2Add = 1;
        }
    }

}
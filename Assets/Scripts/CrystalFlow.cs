using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CrystalFlow : MonoBehaviour, IBonus
    {

        public void StartBonus()
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            StartCoroutine(Bonus());
        }
        private IEnumerator Bonus()
        {

            var mapController = GameObject.FindGameObjectWithTag("GameController");
           
            StartCoroutine(mapController.GetComponent<MapController>().AddBonusCoins());

            var player = GetComponentInParent<Ring>().Player;
            var playerMovement = player.GetComponent<PlayerMovement>();
           
            yield return new WaitForSeconds(PlayerPrefs.GetFloat("SlowDownFactor"));

            mapController.GetComponent<MapController>().RemoveBonusCoins();

        }
    }
}
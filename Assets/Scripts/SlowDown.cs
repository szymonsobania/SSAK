using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class SlowDown : MonoBehaviour, IBonus
    {

        public void StartBonus()
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            StartCoroutine(Bonus());
        }

        private IEnumerator Bonus()
        {
         
            var mapController = GameObject.FindGameObjectWithTag("GameController");
            mapController.GetComponent<MapController>().ChangeSpeed(0.5f);
            var player = GetComponentInParent<Ring>().Player;
            var playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.RotationSpeed = GetComponentInParent<Ring>().rotationSpeed;
            yield return new WaitForSeconds(PlayerPrefs.GetFloat("SlowDownFactor"));
            mapController.GetComponent<MapController>().ChangeSpeed(2f);
            playerMovement.RotationSpeed *= 2;
         
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Could not get Player in Coin.");
            }
            else
            {
                player.AddCoin();
            }
            Destroy(this.gameObject);
        }
    }
}

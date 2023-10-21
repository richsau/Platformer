using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private GameObject _respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.RemoveLife();
            }
            else
            {
                Debug.LogError("Could not get Player in DeadZone.");
            }
            
            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
            }
            else
            {
                Debug.LogError("Could not get CharacterController in DeadZone.");
            }
            other.transform.position = _respawnPoint.transform.position;
            StartCoroutine(CCEnableRoutine(cc));
        }
    }

    IEnumerator CCEnableRoutine(CharacterController cc)
    {
        yield return new WaitForSeconds(0.5f);
        cc.enabled = true;
    }
}

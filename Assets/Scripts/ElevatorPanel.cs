using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _upCallButton;
    [SerializeField]
    private MeshRenderer _downCallButton;
    [SerializeField]
    private Elevator _elevator;
    [SerializeField]
    private int _requiredCoins = 8;

    private void Start()
    {
        _upCallButton.material.color = Color.green;
        _downCallButton.material.color = Color.grey;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) && other.GetComponent<Player>().CoinsCollected() >= _requiredCoins)
            {
                if (_upCallButton.material.color == Color.green)  // go down
                {
                    _downCallButton.material.color = Color.green;
                    _upCallButton.material.color = Color.grey;
                    _elevator.CallElevator(true);
                }
                else
                {
                    _downCallButton.material.color = Color.grey; // go up
                    _upCallButton.material.color = Color.green;
                    _elevator.CallElevator(false);
                }
            }
        }
    }
}

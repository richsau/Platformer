using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 0.25f;
    [SerializeField]
    private float _jumpHeight = 20.0f;
    private float _yVelocity;
    private bool _canDoubleJump = false;
    private int _coins = 0;
    private UIManager _uiManager;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Could not get CharacterController in Player.");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("Could not get UIManager in Player.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;

        if (_controller.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // jump key
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        } 
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump) // do a double jump
            {
                _canDoubleJump = false;
                _yVelocity += _jumpHeight + _gravity;
            }
            _yVelocity -= _gravity;
        }
        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }

    public void AddCoin()
    {
        _coins++;
        _uiManager.UpdateCoinDisplay(_coins);
    }

}

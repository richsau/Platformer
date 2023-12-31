﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 0.25f; // 1.0f;
    [SerializeField]
    private float _jumpHeight = 20.0f; //15.0f; 
    private float _yVelocity;
    private bool _canDoubleJump = false;
    [SerializeField]
    private int _coins;
    private UIManager _uiManager;
    [SerializeField]
    private int _lives = 3;
    private Vector3 _direction, _velocity;
    private bool _canWallJump = false;
    private Vector3 _wallSurfaceNormal;
    private float _pushPower = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL."); 
        }

        _uiManager.UpdateLivesDisplay(_lives);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (_controller.isGrounded == true)
        {
            _canWallJump = false;
            _direction = new Vector3(horizontalInput, 0, 0);
            _velocity = _direction * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == false)
            {
                if (_canDoubleJump == true)
                {
                    _yVelocity += _jumpHeight;
                    _canDoubleJump = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == true)
            {
                _yVelocity = _jumpHeight;
                _velocity = _wallSurfaceNormal * _speed;
            }


            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;

        if (_controller.enabled == true)
        {
            _controller.Move(_velocity * Time.deltaTime);
        }
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "MoveableBox")
        {
            Rigidbody body = hit.collider.GetComponent<Rigidbody>();
            if (body != null)
            {
                Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0);
                body.velocity = pushDir * _pushPower; 
            }
            else
            {
                Debug.LogError("Could not get Rigidbody in OnControllerColliderHit.");
            }
        }

        if (_controller.isGrounded == false && hit.transform.tag == "Wall")
        {
            _wallSurfaceNormal = hit.normal;
            Debug.DrawRay(hit.point, hit.normal, Color.blue);
            _canWallJump = true;
        }
    }


    public void AddCoins()
    {
        _coins++;

        _uiManager.UpdateCoinDisplay(_coins);
    }

    public void Damage()
    {
        _lives--;

        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    public int CoinsCollected()
    {
        return _coins;
    }
}

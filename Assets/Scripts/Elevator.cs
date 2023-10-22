using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private Transform _topTarget, _bottomTarget;
    private float _speed = 3.0f;
    private bool _goingDown = false;
    private bool _isMoving = false;

    public void CallElevator(bool GoingDown)
    {
        _goingDown = GoingDown;
        _isMoving = true;
    }

    private void FixedUpdate()
    {
        if (_goingDown == true && _isMoving)
        {

            transform.position = Vector3.MoveTowards(transform.position, _bottomTarget.position, _speed * Time.deltaTime);
        }
        else if (_goingDown == false && _isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _topTarget.position, _speed * Time.deltaTime);
        }

        if (transform.position == _topTarget.position && _goingDown == false)
        {
            _isMoving = false;
            _goingDown = true;
        }
        else if (transform.position == _bottomTarget.position && _goingDown == true)
        {
            _isMoving = false;
            _goingDown = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }



}

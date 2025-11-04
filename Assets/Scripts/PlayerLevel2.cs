using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLevel2 : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D _rb;
    Collider2D _col;
    Animator _animator;

    int _jumpsLeft;
    bool _jumpBtnPressed;
    bool _isGrounded;
    bool _wasGroundedLastFrame;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponentInChildren<Collider2D>();
        _animator = GetComponent<Animator>();


    }

    void Update()
    {

        
    }

    

    
}

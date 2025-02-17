using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Public Variables 
    #endregion 

    #region Private Variables 
    [SerializeField] private float _baseMovementSpeed;
    [SerializeField][Range(0.25f, 2f)] private float _movementSpeedMod;

    private Rigidbody2D _body2D;
    private PlayerController _controller;
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovementSetUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        PlayerMove(_controller.HInputValue);
    }
    #endregion

    #region Public Methods
    #endregion 

    #region Private Methods 
    private void PlayerMovementSetUp()
    {
        _controller = GetComponent<PlayerController>();
        _body2D = GetComponent<Rigidbody2D>();
    }

    private void PlayerMove(float pDirection)
    {
        _body2D.velocity = new Vector2(_baseMovementSpeed * pDirection * _movementSpeedMod, _body2D.velocity.y);
    }
    #endregion

    #region Coroutines
    #endregion
}

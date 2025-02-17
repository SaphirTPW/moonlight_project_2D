using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Public Variables 
    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }
    #endregion 

    #region Private Variables 
    [Header("Movement Variables : ")]
    [SerializeField] private float _baseMovementSpeed;
    [SerializeField][Range(0, 0.3f)] private float _movementSmoothing; 
    [SerializeField][Range(0.25f, 2f)] private float _movementSpeedMod;

    [Header("Dash Variables : ")]
    [SerializeField] private DashState _dashState;
    private float _dashTimer;
    [SerializeField] private float _maxDash = 20f;
    [SerializeField] private float _dashForce;
    [SerializeField] private bool _isDashing = false;


    [Header("Jump Variables : ")]
    [SerializeField] private float _jumpFallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultipler = 2.5f;
    [SerializeField] private float _defaultGravityScale;
    [SerializeField] private float _baseJumpForce;
    [SerializeField][Range(0.25f, 2f)] private float _jumpForceMod;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private bool _grounded = false;
    [SerializeField] private LayerMask _groundMask;

    private Vector3 _velocity = Vector3.zero;
    private Vector2 _savedVelocity;

    [Header("Type Variables : ")]
    [SerializeField] private Transform _groundCheck;
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
        GroundChecker();
    }

    private void FixedUpdate()
    {
        PlayerMove(_controller.HInputValue);
        PlayerJump(_controller.JumpInput);
        PlayerDash();
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
        Vector3 targetVelocity = new Vector2(pDirection * _baseMovementSpeed * _movementSpeedMod, _body2D.velocity.y);
        _body2D.velocity = Vector3.SmoothDamp(_body2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);
    }

    private void PlayerJump(bool pJumped)
    {
        if (_body2D.velocity.y < 0)
            _body2D.gravityScale = _jumpFallMultiplier;
        else if (_body2D.velocity.y > 0 && !_controller.HoldJumpInput)
            _body2D.gravityScale = _lowJumpMultipler;
        else
            _body2D.gravityScale = _defaultGravityScale;

        if(_grounded && pJumped)
        {
            _body2D.AddForce(transform.up * _baseJumpForce * _jumpForceMod, ForceMode2D.Impulse);
        }
    }

    private void PlayerDash()
    {
        if (_grounded)
        {
            switch (_dashState)
            {
                case DashState.Ready:
                    if (_controller.DashInput)
                    {
                        _savedVelocity = _body2D.velocity;
                        _body2D.velocity = new Vector2(_body2D.velocity.x * _dashForce * _jumpForceMod,_body2D.velocity.y);
                        _isDashing = true;
                        _dashState = DashState.Dashing;
                    }
                    break;
                case DashState.Dashing:
                    _dashTimer += Time.deltaTime * 3f;
                    if(_dashTimer >= _maxDash)
                    {
                        _dashTimer = _maxDash;
                        _body2D.velocity = _savedVelocity;
                        _isDashing = false;
                        _controller.DashInput = false;
                        _dashState = DashState.Cooldown;
                    }
                    break;
                case DashState.Cooldown:
                    _dashTimer -= Time.deltaTime;
                    if(_dashTimer <= 0)
                    {
                        _dashTimer = 0;
                        _dashState = DashState.Ready;
                    }
                    break;
            }
        }
    }

    private void GroundChecker()
    {
        _grounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundMask);

        if (!_grounded)
            _controller.JumpInput = false;
    }
    #endregion

    #region Coroutines
    #endregion
}

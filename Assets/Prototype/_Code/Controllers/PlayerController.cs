using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public Variables 
    public float HInputValue { get => _hInputValue; set => _hInputValue = value; }
    public bool JumpInput { get => _jumpInput; set => _jumpInput = value; }
    public bool HoldJumpInput { get => _holdJumpInput; set => _holdJumpInput = value; }
    public bool DashInput { get => _dashInput; set => _dashInput = value; }
    public bool AttackInput { get => _attackInput; set => _attackInput = value; }
    #endregion

    #region Private Variables 
    [SerializeField] private float _hInputValue;
    [SerializeField] private bool _jumpInput;
    [SerializeField] private bool _holdJumpInput;
    [SerializeField] private bool _dashInput;
    [SerializeField] private bool _attackInput;
    
    [SerializeField] private bool _canJump = true;
    [SerializeField] private bool _canMove = true;
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovementInput();
        CheckJumpInput();
        CheckDashInput();
        CheckAttackInput();
    }
    #endregion

    #region Public Methods
    #endregion 

    #region Private Methods 
    private void CheckMovementInput()
    {
        if(_canMove)
        _hInputValue = Input.GetAxis("Horizontal");
    }

    private void CheckJumpInput()
    {
        if (_canJump)
        {
            if (Input.GetButton("Jump"))
                _holdJumpInput = true;
            else if (Input.GetButtonUp("Jump"))
                _holdJumpInput = false;

            if (Input.GetButtonDown("Jump"))
                _jumpInput = true;
        }
    }

    private void CheckDashInput()
    {
        if (Input.GetButtonDown("Dash"))
            _dashInput = true;
    }

    private void CheckAttackInput()
    {
        if (Input.GetButtonDown("Attack"))
        {
            _attackInput = true;
        }
    }
    #endregion

    #region Coroutines
    #endregion
}

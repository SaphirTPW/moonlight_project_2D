using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public Variables 
    public float HInputValue { get => _hInputValue; set => _hInputValue = value; }
    #endregion 

    #region Private Variables 
    [SerializeField] private float _hInputValue;
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
    }
    #endregion

    #region Public Methods
    #endregion 

    #region Private Methods 
    private void CheckMovementInput()
    {
        _hInputValue = Input.GetAxis("Horizontal");
    }
    #endregion

    #region Coroutines
    #endregion
}

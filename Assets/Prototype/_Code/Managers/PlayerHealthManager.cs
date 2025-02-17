using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthManager : MonoBehaviour
{
    #region Public Variables 
    public float DefenseMod { get => _defenseMod; set => _defenseMod = value; }
    #endregion 

    #region Private Variables 
    [SerializeField] private float _playerMaxHealth;
    [SerializeField] private float _playerCurrentHealth;
    [SerializeField] private TMP_Text _debugPlayerHealthText;
    [SerializeField] private float _defenseMod = 1f;
    private bool _isDead = false;
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        SetPlayerHealth();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerHealth();
    }
    #endregion

    #region Public Methods
    public void PlayerTakeDamage(float pDamage)
    {
        _playerCurrentHealth -= pDamage * _defenseMod;
    }

    public void PlayerGainHealth(float pHealthAmount)
    {
        _playerCurrentHealth += pHealthAmount;
    }
    #endregion 

    #region Private Methods 
    private void SetPlayerHealth()
    {
        _playerCurrentHealth = _playerMaxHealth;

        if (_playerCurrentHealth >= _playerMaxHealth)
            _playerCurrentHealth = _playerMaxHealth;
    }

    private void UpdatePlayerHealth()
    {
        _debugPlayerHealthText.text = Mathf.Round(_playerCurrentHealth).ToString();

        if(_playerCurrentHealth <= 0)
        {
            _playerCurrentHealth = 0f;
            _isDead = true;
        }
    }
    #endregion

    #region Coroutines
    #endregion
}

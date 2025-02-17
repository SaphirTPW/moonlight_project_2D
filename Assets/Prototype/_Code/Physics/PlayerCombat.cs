using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    #region Public Variables 
    public enum AttackState
    {
        Ready,
        Attacking,
        Cooldown
    }
    #endregion 

    #region Private Variables 
    private PlayerController _controller;
    private PlayerMovement _movement;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _attackRange;
    [SerializeField] private AttackState _attackState;
    private bool _isAttacking = false;
    private float _attackTime;
    [SerializeField] private float _attackMaxTime;

    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackMod = 1f;
    [SerializeField] private float _playerComboCounter;
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<PlayerController>();
        _movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack(_attackDamage);
    }
    #endregion

    #region Public Methods
    #endregion 

    #region Private Methods 
    private void PlayerAttack(float pDamage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);

        if (_controller.AttackInput)
        {
            switch (_attackState)
            {
                case AttackState.Ready:
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        enemy.GetComponent<EnemyHealthManager>().TakeDamage(pDamage * _attackMod);
                    }
                    _isAttacking = true;
                    _attackState = AttackState.Attacking;
                    break;
                case AttackState.Attacking:
                    _attackTime += Time.deltaTime * 3f;
                    if(_attackTime >= _attackMaxTime)
                    {
                        _attackTime = _attackMaxTime;
                        _isAttacking = false;
                        _controller.AttackInput = false;
                        _attackState = AttackState.Cooldown;
                    }
                    break;
                case AttackState.Cooldown:
                    _attackTime -= Time.deltaTime;
                    if(_attackTime <= 0)
                    {
                        _attackTime = 0;
                        _attackState = AttackState.Ready;
                    }
                    break;
            }
        }
        #endregion

        #region Coroutines
        #endregion
    }
}
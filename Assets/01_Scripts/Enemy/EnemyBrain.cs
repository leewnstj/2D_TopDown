using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : PoolableMono
{
    public Transform Target;

    public UnityEvent<Vector2> OnMovementKeyPress;
    public UnityEvent<Vector2> OnPointerPositionChanged; //���콺 ���� ��ȯ

    public UnityEvent OnAttackButtonPress = null; //�� �༮ ���ุ ���ֱ�

    public UnityEvent OnResetPool = null;

    public Transform BasePosition; //�Ÿ� ������ ���� �ٴڿ���

    public AIState CurrentState;

    private EnemyRenderer _enemyRenderer;

    [SerializeField] private bool _isActive = false;

    private void Awake()
    {
        _enemyRenderer = transform.Find("VisualSprite").GetComponent<EnemyRenderer>();
    }

    private void Start()
    {
        Target = GameManager.Instance.PlayerTrm;
        CurrentState?.SetUp(transform);
    }

    public void ChangeState(AIState nextState)
    {
        CurrentState = nextState;
        CurrentState?.SetUp(transform);
    }

    public void Update()
    {
        if (_isActive == false) return; //_isActiv�� false�� ������Ʈ ���� ����.

        if(Target == null)
        {
            OnMovementKeyPress?.Invoke(Vector2.zero);
        }
        else
        {
            CurrentState.UpdateState(); //���� ���� ����
        }
    }

    public void Move(Vector2 moveDirection, Vector2 targetPosition)
    {
        OnMovementKeyPress?.Invoke(moveDirection);
        OnPointerPositionChanged?.Invoke(targetPosition);
    }

    public override void Init()
    {
        _isActive = false;
        _enemyRenderer.Reset();

        OnResetPool?.Invoke();
    }

    public void ShowEnemy()
    {
        _isActive = false;
        _enemyRenderer.ShowProgress(2f, () => _isActive = true);
    }

    public void Attack()
    {
        OnAttackButtonPress?.Invoke();
    }

    public void Dead()
    {
        _isActive = false;
    }

    public void GotoPool()
    {
        PoolManager.Instance.Push(this);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[Serializable]
public struct OverlapSettings
{
    [field: SerializeField] public Transform Point { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    [field: SerializeField] public int MaxOverlapColliders { get; private set; }
}

[Serializable]
public struct RaycastSettings
{
    [field: SerializeField] public Transform origin { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public LayerMask LayerMask { get; private set; }
}

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private MeleeDamageCharacteristic damageCharacteristic;

    [SerializeField] private OverlapSettings _overlapSettings;
    [SerializeField] private RaycastSettings _raycastSettings;

    [SerializeField] private MeleeAnimator _animator;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private bool _attacking;

    public event Action Attacked;
    public event Action Hitted;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _animator.AttackAnimation();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_overlapSettings.Point.position, _overlapSettings.Range);
    }

    public void PerformAttack()
    {
        Attacked?.Invoke();
        Collider[] others = new Collider[_overlapSettings.MaxOverlapColliders];

        Physics.OverlapSphereNonAlloc(_overlapSettings.Point.position, _overlapSettings.Range, others,
            _overlapSettings.LayerMask);

        for (int i = 0; i < others.Length; i++)
        {
            if (others[i] == null)
                continue;
            if (others[i].TryGetComponent<HitBox>(out HitBox HitBox))
            {
                if (IsHitBoxBlocked(HitBox))
                {
                    HitBox.Hit(damageCharacteristic.CurrentValue);
                    Hitted?.Invoke();
                }
            }
        }
    }

    public bool IsHitBoxBlocked(HitBox hitBox)
    {
        if (Physics.Raycast(_raycastSettings.origin.position,
            hitBox.transform.position - _raycastSettings.origin.position,
            out RaycastHit hit, _raycastSettings.Range, _raycastSettings.LayerMask))
        {
            if (hit.collider.TryGetComponent<HitBox>(out HitBox HitBox) && hitBox == HitBox)
                return true;
        }

        return false;
    }

    public void StartCheckForAttack()
    {
        _attacking = false;
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0))
            {
                _disposable.Clear();
                _attacking = true;
            }
        }).AddTo(_disposable);
    }

    public void StopCheckForAttack()
    {
        if (!_attacking)
            _animator.IdleAnimation();
        _disposable.Clear();
    }

    private void OnDisable()
    {
        _disposable?.Clear();
    }
}
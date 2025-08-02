using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public abstract class Ultimate
{
    [field: SerializeField] public Material Material { get; private set; }

    protected Transform Player { get; private set; }

    public virtual void Activate(Transform player)
    {
        Player = player;
    }

    public abstract void PerformUltimate();
}

[Serializable]
public class LaserUltimate : Ultimate
{
    [SerializeField] private RaycastSettings[] _eyesRaycastSettingses;

    [SerializeField] private Transform[] _eyes;

    [SerializeField] private LineRenderer[] _lineRenderers;

    [SerializeField] private int _rate;
    [SerializeField] private int _duration;

    [SerializeField] private double _damageRate;
    [SerializeField] private int _damage;

    private CompositeDisposable _firstDisposable = new CompositeDisposable();
    private CompositeDisposable _secondDisposable = new CompositeDisposable();

    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    public override void Activate(Transform player)
    {
        base.Activate(player);
        WaitingForUltimate();
    }

    public async UniTask WaitingForUltimate()
    {
        while (true)
        {
            for (int i = 0; i < _eyes.Length; i++)
            {
                _lineRenderers[i].gameObject.SetActive(false);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(_rate), cancellationToken: _cancellationTokenSource.Token);

            PerformUltimate();
            await UniTask.Delay(TimeSpan.FromSeconds(_duration), cancellationToken: _cancellationTokenSource.Token);
            _firstDisposable.Clear();
            _secondDisposable.Clear();
        }
    }


    public override void PerformUltimate()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            for (int i = 0; i < _eyes.Length; i++)
            {
                _lineRenderers[i].gameObject.SetActive(true);
                _lineRenderers[i].SetPosition(0, _eyes[i].position);
            }

            for (int i = 0; i < _eyesRaycastSettingses.Length; i++)
            {
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(_eyesRaycastSettingses[i].origin.position, Player.position - _eyes[i].position,
                    out hit, _eyesRaycastSettingses[i].Range, _eyesRaycastSettingses[i].LayerMask))
                {
                    if (hit.collider.TryGetComponent<PlayerHitBox>(out PlayerHitBox PlayerHitBox))
                    {
                        _lineRenderers[i].SetPosition(1, PlayerHitBox.transform.position);
                    }
                    else
                    {
                        _lineRenderers[i].SetPosition(1, hit.point);
                    }
                }
            }
        }).AddTo(_firstDisposable);

        Observable.Interval(TimeSpan.FromSeconds(_damageRate)).Subscribe(_ =>
        {
            for (int i = 0; i < _eyesRaycastSettingses.Length; i++)
            {
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(_eyesRaycastSettingses[i].origin.position, Player.position - _eyes[i].position,
                    out hit, _eyesRaycastSettingses[i].Range, _eyesRaycastSettingses[i].LayerMask))
                {
                    if (hit.collider.TryGetComponent<PlayerHitBox>(out PlayerHitBox PlayerHitBox))
                    {
                        Debug.LogError("DAMAGING");
                        Damaging(PlayerHitBox);
                    }
                }
            }
        }).AddTo(_secondDisposable);
    }

    public void Damaging(PlayerHitBox hitBox)
    {
        hitBox.Hit(_damage);
    }

    ~LaserUltimate()
    {
        _cancellationTokenSource?.Cancel();
        _firstDisposable?.Clear();
        _secondDisposable.Clear();
    }
}

[Serializable]
public class EnemyUltimate
{
    [SerializeReference] [SerializeReferenceButton]
    private Ultimate[] _ultimates;

    public Ultimate CurrentUltimate { get; private set; }

    public void Init()
    {
        int random = Random.Range(0, _ultimates.Length);
        CurrentUltimate = _ultimates[random];
        CurrentUltimate.Activate(PlayerCharacter.Instance.LaserPoint);
    }

    ~EnemyUltimate()
    {
        CurrentUltimate = null;
    }
}
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

    [SerializeField] private int _damageRate;
    [SerializeField] private int _damage;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    private CancellationTokenSource[] _damagingCancellationTokenSources = new CancellationTokenSource[]
        {new CancellationTokenSource(), new CancellationTokenSource()};

    private bool[] _damaging = new[] {false, false};

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
            _disposable.Clear();
            for (int i = 0; i < _damaging.Length; i++)
            {
                StopDamaging(i);
            }
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
                        if (!_damaging[i])
                        {
                            _damaging[i] = true;
                            Damaging(PlayerHitBox, i);
                        }
                    }
                    else
                    {
                        StopDamaging(i);
                        _lineRenderers[i].SetPosition(1, hit.point);
                    }
                }
            }
        }).AddTo(_disposable);
    }

    public void StopDamaging(int index)
    {
        Debug.LogError("Stopped");
        _damaging[index] = false;
        _damagingCancellationTokenSources[index]?.Cancel();
    }

    public async void Damaging(PlayerHitBox hitBox, int damagingBoolIndex)
    {
        for (int i = 0; i < _damagingCancellationTokenSources.Length; i++)
        {
            _damagingCancellationTokenSources[i] = new CancellationTokenSource();
        }

        while (_damaging[damagingBoolIndex])
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_damageRate),
                cancellationToken: _damagingCancellationTokenSources[damagingBoolIndex].Token);
            hitBox.Hit(_damage);
        }
    }

    ~LaserUltimate()
    {
        for (int i = 0; i < _damagingCancellationTokenSources.Length; i++)
        {
            _damaging[i] = false;
            _damagingCancellationTokenSources[i].Cancel();
        }

        _cancellationTokenSource?.Cancel();
        _disposable?.Clear();
    }
}

[Serializable]
public class EnemyUltimate
{
    [SerializeReference] [SerializeReferenceButton]
    private Ultimate[] _ultimates;

    private Ultimate _currentUltimate;

    public void Init()
    {
        int random = Random.Range(0, _ultimates.Length);
        _currentUltimate = _ultimates[random];
        _currentUltimate.Activate(PlayerCharacter.Instance.LaserPoint);
    }
}
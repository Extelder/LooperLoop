using System;
using System.Collections;
using System.Collections.Generic;
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

    private CompositeDisposable _disposable = new CompositeDisposable();

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

            await UniTask.Delay(TimeSpan.FromSeconds(_rate));

            PerformUltimate();
            await UniTask.Delay(TimeSpan.FromSeconds(_duration));
            _disposable.Clear();
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
                    _lineRenderers[i].SetPosition(1, hit.point);
                }
            }
        }).AddTo(_disposable);
        Debug.LogError("ULTIMATE");
    }

    ~LaserUltimate()
    {
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
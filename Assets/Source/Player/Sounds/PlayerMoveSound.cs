using System.Collections;
using System.Collections.Generic;
using EvolveGames;
using UniRx;
using UnityEngine;

public class PlayerMoveSound : MonoBehaviour
{
    [SerializeField] private float _rate;
    
    [SerializeField] private AudioSource _moveSource;
    [SerializeField] private PlayerController _controller;

    private CompositeDisposable _waitForGroundedDisposable = new CompositeDisposable();

    private bool _playing;

    private void OnEnable()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (_controller.characterController.isGrounded && _controller.Moving)
            {
                if (_playing)
                    return;
                _playing = true;
                StartCoroutine(PlayingSound());
            }
            else
            {
                _playing = false;
                StopAllCoroutines();
            }
        }).AddTo(_waitForGroundedDisposable);
    }

    private IEnumerator PlayingSound()
    {
        while (true)
        {
            _moveSource.Play();
            yield return new WaitForSeconds(_rate);
        }
    }

    private void OnDisable()
    {
        _waitForGroundedDisposable?.Clear();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//REFACTOR
public class PlayerGuide : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _guideText;


    private bool _ebakaSpawned;
    private bool _ebakaKilled;
    private bool _vendingDetected;
    private bool _vendingBuffBought;

    public static event Action VendingBeginCurrentGoal;
    public static event Action VendingStopCurrentGoal;
    public static event Action EbakaInvokerBeginCurrentGoal;
    public static event Action EbakaInvokerStopCurrentGoal;

    private void OnEbakaSpawned() => _ebakaSpawned = true;
    private void OnEnemyKilled() => _ebakaKilled = true;

    private void Start()
    {
        StartCoroutine(Guiding());
    }

    private void OnDisable()
    {
        EbakaInvoker.EbakaSpawned -= OnEbakaSpawned;
        Enemy.Killed -= OnEnemyKilled;
        VendingMachineBuyable.VendingDetected -= OnVendingDetected;
        VendingMachineBuyable.VendingBuffBought -= OnVendingBuffBought;
    }

    private IEnumerator Guiding()
    {
        yield return new WaitForSeconds(3);
        _guideText.text = "Find and INTERACT nearest Enemy Statue";
        EbakaInvoker.EbakaSpawned += OnEbakaSpawned;
        EbakaInvokerBeginCurrentGoal?.Invoke();
        
        yield return new WaitUntil(() => _ebakaSpawned == true);
        
        EbakaInvokerStopCurrentGoal?.Invoke();
        _guideText.text = "Kill it";
        Enemy.Killed += OnEnemyKilled;
        
        yield return new WaitUntil(() => _ebakaKilled == true);
        
        _guideText.text = "Find nearest Buff Vending Machine";
        VendingBeginCurrentGoal?.Invoke();
        VendingMachineBuyable.VendingDetected += OnVendingDetected;
        
        yield return new WaitUntil(() => _vendingDetected == true);
        
        _guideText.text = "Buy some buff";
        VendingStopCurrentGoal?.Invoke();
        VendingMachineBuyable.VendingBuffBought += OnVendingBuffBought;
        
        yield return new WaitUntil(() => _vendingBuffBought == true);
        
        _guideText.text = "Here`s your loop, your reach in top off screen, Good luck!";
        
        yield return new WaitForSeconds(1f);
        _guideText.text = "";
    }

    private void OnVendingBuffBought() => _vendingBuffBought = true;

    private void OnVendingDetected() => _vendingDetected = true;
}
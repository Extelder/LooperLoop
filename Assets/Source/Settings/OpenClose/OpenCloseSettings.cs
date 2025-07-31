using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseSettings : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenClose();
        }
    }

    public void OpenClose()
    {
        _panel.SetActive(!_panel.activeSelf);

        float time = _panel.activeSelf ? 0 : 1;
        CursorLockMode lockMode = _panel.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        bool visible = _panel.activeSelf;

        Cursor.lockState = lockMode;
        Cursor.visible = visible;

        Time.timeScale = time;
    }
}
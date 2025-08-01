using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(LookAtConstraint))]
public class MeleeInfoPanelLookAt : MonoBehaviour
{
     private LookAtConstraint _lookAtConstraint;

    private void Awake()
    {
        _lookAtConstraint = GetComponent<LookAtConstraint>();
        ConstraintSource source = new ConstraintSource
            {sourceTransform = PlayerCharacter.Instance.Controller.Camera, weight = 1.0f};

        _lookAtConstraint.AddSource(source);
    }
}
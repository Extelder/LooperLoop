using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public void Hit(int damage)
    {
        Debug.LogError(gameObject.name + " " + damage);
    }
}
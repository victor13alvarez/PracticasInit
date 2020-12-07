using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    Animator _pAnim;
    public bool _pray { get; set; }
    private void Awake()
    {
        _pAnim = GetComponent<Animator>();
    }
    // Start is called before the first frame update

    public void SetTriggerAnim(string v)
    {
        _pAnim.SetTrigger(v);
    }
}

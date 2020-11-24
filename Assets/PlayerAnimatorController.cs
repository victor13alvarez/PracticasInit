using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    Animator _pAnim;
    public bool Pray;
    private void Awake()
    {
        _pAnim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _pAnim.SetBool("Pray", Pray);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

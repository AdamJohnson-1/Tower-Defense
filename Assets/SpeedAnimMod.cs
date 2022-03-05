using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAnimMod : MonoBehaviour
{
    private Animator animator;
    public float speedModAmount = 1f;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.speed = animator.speed * speedModAmount;
    }
}

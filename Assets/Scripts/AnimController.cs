using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isWalking;
    private bool isAttack;
    private Movement movement;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        
    }
    private void Awake()
    {
        movement = GetComponentInParent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        isWalking = movement.isWalking;
        isAttack = movement.isAttack;


        if (isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (isAttack)
        {
            animator.SetBool("isAttack", true);
        }
        else
        {
            animator.SetBool("isAttack", false);
        }

    }
}

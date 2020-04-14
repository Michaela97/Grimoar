using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 15f;
    public Animator animator;
    private int isRunningHash = Animator.StringToHash("IsRunning");
    private int attackHash = Animator.StringToHash("Attack");
    Transform target;

    NavMeshAgent agent;
    
    private AudioManager _audioManager;
    

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _audioManager = FindObjectOfType<AudioManager>();
        //_audioManager = gameObject.AddComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyStats.EnemyIsDead)
        {
            enabled = false;
            return;
        }

        FollowPlayer();
    }

    private void FollowPlayer()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            SetRunningAnimation(true);
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance + 4)
            {
                agent.SetDestination(transform.position);
                FaceTarget();
                
                SetRunningAnimation(false);
                SetAttackingAnimation(true);
            }
        }
        else
        {
            SetAttackingAnimation(false);
            SetRunningAnimation(false);
        }
    }

    private void SetRunningAnimation(bool isRunning)
    {
        {
            if (isRunning)
            {
                animator.SetFloat(isRunningHash, 1);
                _audioManager.Play("EnemyFootSteps");
            }
            else
            {
                animator.SetFloat(isRunningHash, 0);
                _audioManager.Stop("EnemyFootSteps");
            }
        }
    }

    private void SetAttackingAnimation(bool isAttacking)
    {
        {
            if (isAttacking)
            {
                animator.SetBool(attackHash, true);
            }
            else
            {
                animator.SetBool(attackHash, false);
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //radius around enemy 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
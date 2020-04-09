using System;
using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        
        if (distance <= lookRadius)
        {
           setRunningAnimation(true);
           agent.SetDestination(target.position);

           if (distance <= agent.stoppingDistance + 4)
            {
                agent.SetDestination(transform.position); 
                // Debug.Log("distance: "  + distance +  "  / agent stopping distance: " + agent.stoppingDistance);
                FaceTarget();
                setRunningAnimation(false);
                setAttackingAnimation(true);
            }
        }
        else
        {
            setAttackingAnimation(false);
            setRunningAnimation(false);
        }

    }

    private void setRunningAnimation(bool isRunning)
    {
        if (isRunning)
        {
            animator.SetFloat(isRunningHash, 1);
        }
        else
        {
            animator.SetFloat(isRunningHash, 0);
        }
    
    }

    private void setAttackingAnimation(bool isAttacking)
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

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy was hit by player");

        }
        
        else if (other.gameObject.CompareTag("Sword"))
        {
            Debug.Log("Enemy was hit by sword");
        }

    }
    
}

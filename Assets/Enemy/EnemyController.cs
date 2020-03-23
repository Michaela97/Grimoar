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
           setAnimation(true);
           agent.SetDestination(target.position);

           if (distance <= agent.stoppingDistance + 2)
            {
                agent.SetDestination(transform.position); //find better way how to stop him
                Debug.Log("distance: "  + distance +  "  / agent stopping distance: " + agent.stoppingDistance);
                FaceTarget();
                setAnimation(false);
                //Attack
            }
        }
        
    }

    private void setAnimation(bool isRunning)
    {
        if (isRunning)
        {
            animator.SetFloat(isRunningHash, 1);
            Debug.Log("RUNNING");
        }
        else
        {
            animator.SetFloat(isRunningHash, 0);
            Debug.Log("NOT RUNNING");
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
}

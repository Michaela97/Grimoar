using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RabitController : MonoBehaviour
{
    [Range(5,10)]
    [SerializeField]
    private float wanderRadius;
    [Range(3,6)]
    [SerializeField]
    private float wanderTimer;
    [SerializeField]
    private NavMeshAgent agent;
    private float _timer;

    private Animator _animator;
    private readonly int _speedHash = Animator.StringToHash("Speed");
    private Vector3 _newPos;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _timer = 5;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
 
        if (_timer >= wanderTimer) {
            Vector3 newPos = AgentRandomMovement.RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            _timer = 0;
        }
        
        _animator.SetFloat(_speedHash, agent.velocity.magnitude);
    }

}

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
        StartCoroutine(GetNewPosition());
    }

    private void Update()
    { 
        _timer += Time.deltaTime;
        
        if (_timer >= wanderTimer) {
            agent.SetDestination(_newPos);
        }
        _animator.SetFloat(_speedHash, agent.velocity.magnitude);
    }

    private IEnumerator GetNewPosition()
    {
        while (true)
        {
            _newPos = AgentRandomMovement.RandomNavSphere(transform.position, wanderRadius, -1);
            yield return new WaitForSeconds(5);
        }
    }


}

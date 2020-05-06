using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RabitController : MonoBehaviour
{
    [SerializeField] private float randomX = 25;
    [SerializeField] private float randomZ = 25;
    [SerializeField] private float minWaitTime = 2;
    [SerializeField] private int maxWaitTime = 10;
    private Vector3 _newPosition;

    private Animator _animator;
    private int speedHash = Animator.StringToHash("Speed");


    void Start()
    {
        _animator = GetComponent<Animator>();

        
        StartCoroutine(PickPosition());
    }

    IEnumerator PickPosition()
    {
        var x = Random.Range(-randomX, randomX);
        var z = Random.Range(-randomZ, randomZ);

        _newPosition = new Vector3( x, 0.23f,  z);

        // Quaternion lookRotation = Quaternion.LookRotation(new Vector3(_newPosition.x, _newPosition.y, _newPosition.z));
        // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        
      //  transform.Rotate(_newPosition);

        
        StartCoroutine(MoveToRandomPos());
        
        yield return null;
    }

    IEnumerator MoveToRandomPos()
    {
        _animator.SetFloat(speedHash, 1);
        
        transform.Translate(_newPosition);

        yield return new WaitForSeconds(5);

        StartCoroutine(WaitForSomeTime());
    }

    IEnumerator WaitForSomeTime()
    {
        var time = Random.Range(minWaitTime, maxWaitTime);
        
        _animator.SetFloat(speedHash, 0);

        yield return new WaitForSeconds(time);

        StartCoroutine(PickPosition());
    }
}
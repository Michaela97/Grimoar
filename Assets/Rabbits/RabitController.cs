using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RabitController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float randomX;
    [SerializeField]
    private float randomZ;
    [SerializeField]
    private float minWaitTime = 2;
    [SerializeField]
    private int maxWaitTime = 10;
    private Vector3 newPosition;
    public Transform rabbitBody;

    private Animator _animator;
    private int speedHash = Animator.StringToHash("Speed");
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        
        PickPosition();
    }

    void PickPosition()
    {
        var currentPosition = gameObject.transform.position;

        var x = Random.Range(-randomX, randomX);
        var z = Random.Range(-randomZ, randomZ);
 
        newPosition = new Vector3(currentPosition.x + x, 0, currentPosition.z + z);
  
        StartCoroutine(MoveToRandomPos());
    }

    void RandomRotation() //TODO
    {

        var y = Random.Range(0, 180);
        //transform.Rotate(0f, y, 0f);
        //transform.rotation = Quaternion.Euler(0, y, 0);
        //transform.localRotation = Quaternion.Euler(0f, y, 0f); 
        //rabbitBody.Rotate(Vector3.right * 2 * Time.deltaTime, Space.Self);
        //Debug.Log("Rotation value: "  + y);
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(0, y, 0));
        rabbitBody.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    IEnumerator MoveToRandomPos()
    {
        var i = 0.0f;
        var rate = 1.0f / speed;
        _animator.SetFloat(speedHash, 1);
        
        var walkingTime = Random.Range(2.0f, 8.0f);
        
        while (i < walkingTime)
        {
            i += Time.deltaTime * rate;
            transform.forward = newPosition * i;
            yield return null;
        }
        
        StartCoroutine(WaitForSomeTime());
    }
    
    IEnumerator WaitForSomeTime()
        {
            _animator.SetFloat(speedHash, 0);
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            
            RandomRotation();
            PickPosition();
        }
    
}

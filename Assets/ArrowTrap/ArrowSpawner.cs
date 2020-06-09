using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.AI;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    private Transform _target;
    [Range(15, 20)] [SerializeField] private float lookRadius;

    void Start()
    {
        _target = PlayerManager.instance.player.transform;
    }

    void Update()
    {

        int layerMask = 1 << 8;
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        
        RaycastHit hit;
        
        float distance = Vector3.Distance(_target.position, transform.position);

        if (distance <= lookRadius)
        {
            
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, _target.position * hit.distance, Color.yellow);
                PlayerCombat.IsHit = true;
            }
            else
            {
                Debug.DrawRay(transform.position,_target.position * 1000, Color.white);
                PlayerCombat.IsHit = false;
            }
        }
    }

    //TODO - this will be shooting arrows (next version)
    private GameObject SpawnArrow()
    {
        var position = new Vector3(-10f, 5.8f, -19f);
        GameObject newArrow = Instantiate(arrow, position, Quaternion.identity);
        return newArrow;
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
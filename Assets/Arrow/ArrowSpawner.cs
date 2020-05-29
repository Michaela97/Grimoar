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
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        
        RaycastHit hit;
        
        float distance = Vector3.Distance(_target.position, transform.position);

        if (distance <= lookRadius)
        {

            //hit is struct so use 'out' to pass by reference, instead of default pass by copy 
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, _target.position * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                PlayerCombat.IsHit = true;
            }
            else
            {
                Debug.DrawRay(transform.position,_target.position * 1000, Color.white);
                Debug.Log("Did not Hit");
                PlayerCombat.IsHit = false;
            }
        }
    }

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
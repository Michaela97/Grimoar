using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class AgentRandomMovement 
{
    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layer) {
        
        Vector3 randDirection = Random.insideUnitSphere * distance;
        randDirection += origin;
        
        NavMeshHit navHit;
        NavMesh.SamplePosition (randDirection, out navHit, distance, layer);
        
        return navHit.position;
    }
}

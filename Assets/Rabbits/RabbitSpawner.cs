using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawner : MonoBehaviour
{
    public GameObject rabbit;
    void Start()
    {
        SpawnRabbit();
    }

    private void SpawnRabbit()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject pot = Instantiate(rabbit);
            GetRandomPosition(pot);
        }
    }
    
    private void GetRandomPosition(GameObject gameObject)
    {
        Vector3 position = new Vector3(GetRandomNumber()+50f, 0.23f, GetRandomNumber()+25f);
        gameObject.transform.position = position;
    }
    
    private float GetRandomNumber()
    {
        return Random.Range(-300f, 300f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    public GameObject potion;
    void Start()
    {
        SpawnPotion();
    }

    private void SpawnPotion()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject pot = Instantiate(potion);
            GetRandomPosition(pot);
            
        }
    }
    
    private void GetRandomPosition(GameObject gameObject)
    {
        Vector3 position = new Vector3(GetRandomNumber(), 2.7f, GetRandomNumber());
        gameObject.transform.localPosition = position;
    }
    
    private float GetRandomNumber()
    {
        return Random.Range(-100f, 100f);
    }
}

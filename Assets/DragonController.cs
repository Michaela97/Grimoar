using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem fire;
    // Start is called before the first frame update
    void Start()
    {
        fire = GameObject.Find("Fire").GetComponent<ParticleSystem>();
        StartCoroutine(StartFire());
    }
    IEnumerator StartFire()
    {
        while (true)
        {
            fire.Play();
            
            yield return new WaitForSeconds(5);

            fire.Stop();
            
            yield return new WaitForSeconds(5);
        }
    }
}

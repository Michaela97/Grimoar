
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DragonController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem fire;
    [Range(5,10)]
    [SerializeField]
    private float wanderRadius;
    [Range(3,6)]
    [SerializeField]
    private float wanderTimer;
    [SerializeField]
    private NavMeshAgent agent;
    private float _timer;
    private AudioManager _audioManager;

    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        fire = GameObject.Find("Fire").GetComponent<ParticleSystem>();
        agent = GetComponent<NavMeshAgent>();
        _timer = wanderTimer;
        StartCoroutine(StartFire());
    }

    private void Update()
    {
        _timer += Time.deltaTime;
 
        if (_timer >= wanderTimer) {
            Vector3 newPos = AgentRandomMovement.RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            _timer = 0;
        }
    }
    IEnumerator StartFire()
    {
        while (true)
        {
            fire.Play();
            _audioManager.Play("FireSound");
            yield return new WaitForSeconds(5);

            fire.Stop();
            _audioManager.Stop("FireSound");
            yield return new WaitForSeconds(5);
        }
    }
}

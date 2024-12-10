using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private Collider coll;
    private RaycastHit hit;
    private bool isDead;

    [SerializeField] private AudioClip dieClip;
    private AudioSource source;

    private Transform player;

    [SerializeField] private float chaseRadius;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider>();
        source = GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isDead)
            return;

        if (player == null)
            return;

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance > chaseRadius)
            return;

        agent.SetDestination(player.position);
        Attack();
    }

    private void Attack()
    {
        Vector3 vectorToPlayer = player.position - transform.position;

        if (Physics.Raycast(transform.position, vectorToPlayer.normalized, out hit, 1.5f))
        {
            if (hit.collider.CompareTag("Player"))
                hit.collider.GetComponent<Player>().Kill();
        }
    }

    public void Kill()
    {
        isDead = true;
        coll.enabled = false;
        agent.enabled = false;
        animator.SetTrigger("Die");
        source.PlayOneShot(dieClip);
    }
}

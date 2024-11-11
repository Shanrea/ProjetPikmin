using UnityEngine;
using UnityEngine.AI;

public class PikminFollow : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private bool isFollowing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isFollowing)
        {
            agent.SetDestination(player.position);
        }
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    public void StopFollowing()
    {
        isFollowing = false;
    }
}


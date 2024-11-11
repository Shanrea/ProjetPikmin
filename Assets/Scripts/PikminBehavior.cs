using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PikminBehavior : MonoBehaviour
{
    public float speed = 5.0f; // Vitesse de déplacement
    public Transform target; // Cible du Pikmin
    private NavMeshAgent agent;
    public float wallAvoidanceStrenght = 5.0f;

    void Start() 
    { 
        agent = GetComponent<NavMeshAgent>(); 
    }

    void Update()
    {
        MoveTowardsTarget();
        if (target != null) 
        {
            agent.SetDestination(target.position); 
        }
        AvoidWall();
    }

    void MoveTowardsTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            // Logique pour ramasser l'objet
            CollectItem(other.gameObject);
        }
    }

    void CollectItem(GameObject item)
    {
        // Ajouter une animation ou un effet ici
        Destroy(item);
    }
    void AvoidWall()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * agent.radius;

        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, agent.radius + 0.5f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                Vector3 avoidDirection = Vector3.Reflect(transform.forward, hit.normal);
                agent.velocity = avoidDirection * wallAvoidanceStrenght;
            }
        }
        Debug.DrawLine(sensorStartPos, transform.forward * wallAvoidanceStrenght, Color.red);

    }
}

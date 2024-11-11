using UnityEngine;

public class PikminThrowWithAnimation : MonoBehaviour
{
    public Transform player;
    public Camera playerCamera;
    public float throwForce = 10.0f;
    public float targetOffset = 2.0f; // Distance à laquelle le Pikmin doit s'arrêter par rapport au joueur
    private bool isPickedUp = false;
    private Transform originalParent;
    private Renderer pikminRenderer;
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        // Chercher le Renderer et l'Animator sur l'enfant
        pikminRenderer = GetComponentInChildren<Renderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isPickedUp) // Touche E pour ramasser
        {
            PickUp();
        }
        else if (Input.GetMouseButtonDown(0) && isPickedUp) // Clique gauche pour lancer
        {
            Launch();
        }
    }

    void PickUp()
    {
        isPickedUp = true;
        originalParent = transform.parent;
        transform.parent = player;

        // Ajuster la position pour être derrière le joueur
        transform.localPosition = new Vector3(0, 1, -1);
        rb.isKinematic = true;

        if (pikminRenderer != null)
        {
            pikminRenderer.material.color = Color.green; // Changer la couleur pour indiquer le ramassage
        }

        Debug.Log("Pikmin ramassé !");
    }

    void Launch()
    {
        isPickedUp = false;
        transform.parent = originalParent;
        rb.isKinematic = false;

        // Utiliser un raycast pour déterminer la position de la souris dans le monde 3D
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point; // Position cible basée sur le clic de la souris

            // Déclencher l'animation
            if (animator != null)
            {
                animator.SetTrigger("Throw"); // Assure-toi que tu as un trigger appelé "Throw" dans ton Animator
            }

            Debug.Log("Pikmin lancé vers : " + targetPosition);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Assure-toi que le sol a le tag "Ground"
        {
            rb.isKinematic = true; // Arrêter le mouvement
            rb.linearVelocity = Vector3.zero; // Arrêter la vitesse
            rb.angularVelocity = Vector3.zero; // Arrêter la rotation

            Debug.Log("Pikmin a touché le sol et s'est arrêté !");
        }
    }
}

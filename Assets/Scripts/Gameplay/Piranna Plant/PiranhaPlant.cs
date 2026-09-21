using System.Collections;
using UnityEngine;

public class PiranhaPlantController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 1f;          // Speed at which the plant moves
    [SerializeField] private float moveDistance = 2f;       // How far the plant emerges (upward distance)
    [SerializeField] private float pauseTimeAtTop = 1f;     // Time to pause when fully emerged
    [SerializeField] private float pauseTimeAtBottom = 1f;  // Time to pause when retracted

    [Header("Fireball Settings")]
    [SerializeField] private GameObject fireballPrefab;     // Prefab of the fireball
    [SerializeField] private Transform fireballSpawnPoint;  // Position from which the fireball is spawned
    [SerializeField] private float fireballSpeed = 5f;        // Speed of the fireball

    private Vector3 initialPosition;    // Starting (hidden) position of the plant
    private Vector3 topPosition;        // Fully emerged position
    private Transform playerTransform;  // Reference to the player�s transform

    private void Start()
    {
        // Store the initial position and compute the top (emerged) position.
        initialPosition = transform.position;
        topPosition = initialPosition + new Vector3(0, moveDistance, 0);

        // Attempt to find the player by tag if not already assigned.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        // Start the coroutine to handle the up/down movement and firing.
        StartCoroutine(PiranhaRoutine());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // Add fireball collision check
        if (collision.gameObject.CompareTag("Fireball"))
        {
            Destroy(collision.gameObject); // Destroy fireball
            Destroy(gameObject); // Destroy Goomba

        }
    }
    private IEnumerator PiranhaRoutine()
    {
        while (true)
        {
            // Move upward until reaching the top position.
            while (Vector3.Distance(transform.position, topPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, topPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Pause at the top position.
            yield return new WaitForSeconds(pauseTimeAtTop);

            // When fully emerged, shoot a fireball.
            ShootFireball();

            // Optional brief pause after shooting.
            yield return new WaitForSeconds(0.5f);

            // Move downward back to the initial position.
            while (Vector3.Distance(transform.position, initialPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Pause at the bottom before starting the next cycle.
            yield return new WaitForSeconds(pauseTimeAtBottom);
        }
    }

    private void ShootFireball()
    {
        if (fireballPrefab != null && fireballSpawnPoint != null)
        {
            // Instantiate the fireball at the designated spawn point.
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
            // Assign player reference so the fireball tracks the player
            Fireballs fb = fireball.GetComponent<Fireballs>();
            if (fb != null)
            {
                fb.player = playerTransform;
                fb.speed = fireballSpeed;
            }
            else
            {
                // Fallback: just set initial velocity if no Fireballs script
                Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = Vector2.right;
                    if (playerTransform != null)
                        direction = (playerTransform.position - fireballSpawnPoint.position).normalized;
                    rb.linearVelocity = direction * fireballSpeed;
                }
            }
        }
        else
        {
            Debug.LogWarning("FireballPrefab or FireballSpawnPoint not assigned.");
        }
    }


}

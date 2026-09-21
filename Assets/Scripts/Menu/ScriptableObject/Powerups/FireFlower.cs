using UnityEngine;

public class FireFlower : MonoBehaviour
{
    [SerializeField] float destroyDelay = 0.3f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MarioController mario = other.GetComponent<MarioController>();
            if (mario != null)
            {
                mario.EnableFirePower();
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject, destroyDelay);
            }
        }
    }
}
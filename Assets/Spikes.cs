using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float damage = 10f;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerStats"))
        {
            Debug.Log("Player hit a spike");
            collision.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

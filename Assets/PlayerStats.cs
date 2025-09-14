using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float health;
    private bool canTakeDamage = true;
    private Animator animator;
    public Rigidbody2D rigidbody2D;
    public LogicScript logicScript;

    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();  // รับค่า Rigidbody2D เพื่อควบคุมการเคลื่อนไหว
        logicScript = Object.FindFirstObjectByType<LogicScript>();
        logicScript.UpdatePlayerHP(health);
    }

    void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage)
            return;

            

        health -= damage;
        logicScript.UpdatePlayerHP(health);

        if (health <= 0)
        {
            animator.SetBool("Damage", true);
            GetComponent<PolygonCollider2D>().enabled = false;  // ปิด Collider
            GetComponentInParent<Gatherinput>().OnDisable();  // ปิดการควบคุมการเคลื่อนที่
            Debug.Log("Player is dead");

            Die();  // เรียกฟังก์ชัน Die() เมื่อเลือดหมด
        }
        StartCoroutine(DamagePrevention());
    }

    IEnumerator DamagePrevention()
    {
        canTakeDamage = false;  // ป้องกันไม่ให้รับความเสียหายซ้ำ
        yield return new WaitForSeconds(0.15f);  // ชั่วขณะหนึ่งก่อนที่สามารถรับความเสียหายได้ใหม่

        if (health > 0)
        {
            canTakeDamage = true;
            animator.SetBool("Damage", false);  // รีเซ็ตสถานะ Damage
        }
        else
        {
            animator.SetBool("Death", true);  // เมื่อเลือดหมด เรียกอนิเมชัน Death
        }
    }

    // ฟังก์ชันเมื่อ Player ตาย
    private void Die()
    {
        Debug.Log("Player has died");

        // เรียก Trigger ของ Animator เพื่อให้อนิเมชันการตายทำงาน
        animator.SetTrigger("death");  // เรียก Trigger "death"

        // หยุดการเคลื่อนไหวของตัวละคร
        rigidbody2D.velocity = Vector2.zero;  // หยุดการเคลื่อนไหวของ Rigidbody2D

        // หยุดการเคลื่อนที่ของตัวละคร
        this.enabled = false;  // ปิดสคริปต์ PlayerStats เพื่อให้ไม่สามารถเคลื่อนที่ได้

        // แสดง UI Game Over
        // ไม่เรียก ShowGameOver ที่นี่แล้ว
        // logicScript.ShowGameOver();

        // ทำลายตัวละครหลังจาก 1 วินาที (ให้อนิเมชันการตายมีเวลาแสดง)
        Destroy(gameObject, 1f);
    }

    // ฟังก์ชันหยุดการเคลื่อนไหวของตัวละคร (สำหรับ Game Over)
}

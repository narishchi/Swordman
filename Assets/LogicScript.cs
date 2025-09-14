using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public Text hpText;  // UI Text ที่ใช้แสดงค่า HP
    public GameObject gameOverUI;  // UI Game Over ที่จะทำให้แสดงขึ้น
    public PlayerStats playerStats;  // อ้างอิงไปยัง PlayerStats

    private bool isGameOver = false;  // ตัวแปรเช็คว่าเกมจบแล้วหรือยัง

    // ฟังก์ชันที่ใช้ในการอัปเดตค่า HP
    public void UpdatePlayerHP(float hp)
    {
        hpText.text = Mathf.Round(hp).ToString();  // แสดงค่า HP ใน UI

        // เมื่อเลือดเป็น 0, เรียกฟังก์ชัน Game Over
        if (hp <= 0f && !isGameOver) // ถ้าเลือดเหลือ 0 และยังไม่มีการจบเกม
        {
            gameOver();  // เรียกฟังก์ชัน gameOver
        }
    }
    public void restartGame()
    {
        playerStats.health = playerStats.maxHealth;
        // โหลดฉากปัจจุบัน (เริ่มเกมใหม่)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // โหลดฉากใหม่เพื่อเริ่มเกมใหม่
    }

    // ฟังก์ชันแสดง UI Game Over
    public void gameOver()
    {
        isGameOver = true;  // บอกว่าเกมจบแล้;
        gameOverUI.SetActive(true);  // แสดง UI Game Over
    }

    // ฟังก์ชันเริ่มเกมใหม่
}

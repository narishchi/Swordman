using Mono.Cecil;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    public float speed = 5f;
    private int direction = 1;
    public float jumpForce = 5;
    private bool grounded = false;
    
    private Gatherinput gatherInput;
    private Rigidbody2D rigidbody2D;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gatherInput = GetComponent<Gatherinput>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
        SetAnimatorValues();
        Flip();
        rigidbody2D.velocity = new Vector2(speed * gatherInput.valueX, rigidbody2D.velocity.y);
        JumpPlayer();
    }

    public Transform leftPoint;
    public float rayLength;
    public LayerMask groundLayer;

    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        grounded = leftCheckHit.collider != null;
        print(grounded);
    }

    private void JumpPlayer()
    {
        if (gatherInput.jumpInput)
        {
            rigidbody2D.velocity = new Vector2(gatherInput.valueX * speed, jumpForce);
        }
        gatherInput.jumpInput = false;
    }

    private void SetAnimatorValues()
    {
        animator.SetFloat("speed", Mathf.Abs(rigidbody2D.velocity.x));
        animator.SetFloat("vSpeed", rigidbody2D.velocity.y);
        animator.SetBool("ground", grounded);
    }

    /// <summary>
    /// For flipping the character
    /// </summary>
    private void Flip()
{
    if (gatherInput.valueX * direction < 0)
    {
        // To flip character
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        // Update direction
        direction *= -1;
    }
}
}

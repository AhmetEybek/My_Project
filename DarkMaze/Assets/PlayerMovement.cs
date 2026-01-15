using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioSource footstepAudio;

    public float moveSpeed = 5f;
    public Rigidbody rb;

    private Vector3 moveDirection;

    void Update()
    {
    bool yurumekte = Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;

    if (yurumekte && !footstepAudio.isPlaying)
    {
        footstepAudio.Play();
    }
    else if (!yurumekte && footstepAudio.isPlaying)
    {
        footstepAudio.Stop();
    }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(moveX, 0, moveZ).normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
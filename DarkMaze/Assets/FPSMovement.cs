using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;

    void Update()
    {
        // Klavyeden input al
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Hareket yönünü karakterin kendi yönüne göre ayarla
        Vector3 move = transform.right * x + transform.forward * z;

        // Hareketi uygula
        controller.Move(move * speed * Time.deltaTime);
    }
}
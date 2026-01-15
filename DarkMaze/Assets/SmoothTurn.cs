using UnityEngine;

public class SmoothTurn : MonoBehaviour
{
    public float rotationSpeed = 100f; // saniyede kaç derece dönsün

    void Update()
    {
        float horizontalInput = 0f;

        // Sağ ve sol tuşlara basılıysa dönüş yönü belirle
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }

        // Dönüşü uygula
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
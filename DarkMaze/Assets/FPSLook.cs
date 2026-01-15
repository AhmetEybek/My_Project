using UnityEngine;

public class FPSLook : MonoBehaviour
{
    public Transform playerBody; // karakterin kendisi
    public float mouseSensitivity = 100f;
    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Mouse'u ekran ortasında kilitle
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Yukarı-aşağı bakış (kameraya özel)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Baş aşağı dönmesin

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Karakterin sağa-sola dönüşü
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
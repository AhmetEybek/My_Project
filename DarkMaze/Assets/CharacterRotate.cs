using UnityEngine;

public class CharacterRotate : MonoBehaviour
{
    public float turnSpeed = 5f;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetRotation *= Quaternion.Euler(0, 45, 0); // 90 derece sağa dön
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetRotation *= Quaternion.Euler(0, -45, 0); // 90 derece sola dön
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }
}
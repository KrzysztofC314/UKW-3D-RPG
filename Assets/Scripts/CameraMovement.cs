using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; 
    public float rotationSpeed = 2.0f; 

    private Vector3 rotation = Vector3.zero;

    void Update()
    {
        // Ruch kamery za pomoc¹ klawiszy WASD w osi X i Z
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Obrót kamery za pomoc¹ myszki tylko w osi Y
        if (Input.GetMouseButton(2))
        {
            rotation.y += Input.GetAxis("Mouse X") * rotationSpeed;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}

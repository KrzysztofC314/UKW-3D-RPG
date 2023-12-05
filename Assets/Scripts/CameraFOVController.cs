using UnityEngine;

public class CameraFOVController : MonoBehaviour
{
    public float zoomSpeed = 10f;
    public float minFOV = 10f; 
    public float maxFOV = 60f; 

    void Update()
    {
        // Pobranie z kó³ka myszy
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            scrollInput--;
        }
        else if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            scrollInput++;
        }

        float newFOV = Camera.main.fieldOfView - (scrollInput * zoomSpeed);

        // Ograniczenie wartoœci pola widzenia w zakresie minFOV i maxFOV
        newFOV = Mathf.Clamp(newFOV, minFOV, maxFOV);

        // Zmiana pola widzenia kamery
        Camera.main.fieldOfView = newFOV;
    }
}
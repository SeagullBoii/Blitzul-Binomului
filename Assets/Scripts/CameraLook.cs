using DG.Tweening;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] Transform cam;
    [SerializeField] Transform camTilt;

    float xRot = 0f; //X = coordonata pe verticala a camerei.
    float yRot = 0f; //Y = coordonata pe orizontala a camerei.

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        RotateCamera();
    }


    /// <summary>
    /// Functie pentru a roti camera in functie de Mouse.
    /// Pentru a misca camera pe orizontala inmultim cu Vector3.up (coordonata Y este de rotire stanga-dreapta);
    /// Rotim corpul jucatorului pe orizontala si camera pe verticala.
    /// Scadem mouseY pentru ca este inverted pe plan.
    /// Dam Clamp pentru a limita vederea pe verticala, ca jucatorul sa nu se uite in spate.
    /// </summary>

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 0.02f;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 0.02f;

        yRot += mouseX;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRot, 0);
    }

    //Folosim DOTween pentru a inclina camera
    public void TiltCamera(float x, float y, float z, float speed)
    {
        camTilt.DOLocalRotate(new Vector3(x, y, z), speed);
    }
}

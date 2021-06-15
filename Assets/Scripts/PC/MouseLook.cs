using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float sensitivity = 100;
    [SerializeField] bool invertX;
    [SerializeField] bool invertY;

    bool active = false;

    float verticalAngle;

    Transform player;
    public void EnableMouseLook(Transform _player)
    {
        player = _player;
        active = true;
        SetMouseVisible(false);
    }

    public void SetMouseLookEnabled(bool _enabled)
    {
        active = _enabled;
        SetMouseVisible(!_enabled);
    }


    private void Update()
    {
        if (active)
        {
            MouseLookHorizontal();
            MouseLookVertical();
        }
    }

    public void SetMouseVisible(bool _visible)
    {
        Cursor.visible = _visible;
        Cursor.lockState = _visible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void MouseLookHorizontal()
    {
        //Get the mouse X position
        float mouseX = Input.GetAxis("Mouse X");

        //multiply by sensitivity
        mouseX *= sensitivity;

        //multiply by -1 if invert x
        if (invertX) mouseX *= -1;

        //Rotate player body
        player.Rotate(Vector3.up * mouseX);
    }

    void MouseLookVertical()
    {
        //Get the mouse Y position
        float mouseY = Input.GetAxis("Mouse Y");

        //multiply by sensitivity
        mouseY *= sensitivity;

        //Change vertical angle by mouse Y, negative if no invert Y
        verticalAngle += mouseY * (invertY ? 1 : -1);

        //Clamp vertical angle so the player can't flip the camera around
        verticalAngle = Mathf.Clamp(verticalAngle, -90, 90);

        //Set camera rotation
        transform.localRotation = Quaternion.Euler(verticalAngle, 0, 0);
    }
}

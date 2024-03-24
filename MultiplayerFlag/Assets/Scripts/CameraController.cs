using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private Transform cameraTarget;
    public float targetHeight;
    private Vector2 targetRotation;

    public void CameraRotation(Vector2 dir)
    {
        targetRotation += dir;
    }

    void LateUpdate()
    {
        cameraTarget.transform.position = characterMovement.transform.position + Vector3.up * targetHeight;
    }

    void Update()
    {
        cameraTarget.transform.rotation = Quaternion.Euler(targetRotation.x, targetRotation.y, 0);
    }
}

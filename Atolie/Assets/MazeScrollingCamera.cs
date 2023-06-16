using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeScrollingCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform followTransform;
    [SerializeField] private RectTransform screen;

    private void Awake()
    {
        float scale = screen.rect.width / screen.rect.height;
        cameraTransform.GetComponent<Camera>().aspect = scale;
        cameraTransform.GetComponent<Camera>().ResetAspect();
    }

    private void Update()
    {
        cameraTransform.position = new Vector3(followTransform.position.x, followTransform.position.y, cameraTransform.position.z);
    }
}

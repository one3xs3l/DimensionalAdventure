using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Zoom : MonoBehaviour
{
    [SerializeField] bool ZoomActive;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] float Speed;
    [SerializeField] float ZoomSize;
    [SerializeField] bool IsZoom = false;

    public void LateUpdate()
    {
        if (ZoomActive)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, ZoomSize, Speed * Time.deltaTime);
        }
        else
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 8f, Speed * Time.deltaTime);
        }
    }

    public void ZoomScreen()
    {
        if (IsZoom == false)
        {
            ZoomActive = true;
            IsZoom = true;
        }
        else
        {
            ZoomActive = false;
            IsZoom = false;
        }
    }
}

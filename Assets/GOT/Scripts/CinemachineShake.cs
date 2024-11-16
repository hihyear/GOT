using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera _camera;

    private float _intensity = 5.0f;
    private float _time = 0.3f;

    private void Awake()
    {
        Instance = this;
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin periln = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        periln.m_AmplitudeGain = _intensity;
        StartCoroutine(co_StopShake());
    }

    IEnumerator co_StopShake()
    {
        yield return new WaitForSeconds(_time);
        
        CinemachineBasicMultiChannelPerlin periln = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        periln.m_AmplitudeGain = 0.0f;
        //Mathf.Lerp(_intensity, 0.0f, Time.deltaTime * 2.0f);

    }
}

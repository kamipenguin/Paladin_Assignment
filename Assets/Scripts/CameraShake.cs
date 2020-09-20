using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float _shakeDuration = 0.5f;
    [SerializeField]
    private float _shakeAmplitude = 1.2f;
    [SerializeField]
    private float _shakeFrequency = 2.0f;
    private float _shakeElapsedTime = 0f;

    //cinemachine variables
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _virtualCameraNoise;

    private void Start()
    {
        if (_virtualCamera != null)
        {
            _virtualCameraNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    private void Update()
    {
        if (_virtualCamera != null || _virtualCameraNoise != null)
        {
            if (_shakeElapsedTime > 0)
            {
                _virtualCameraNoise.m_AmplitudeGain = _shakeAmplitude;
                _virtualCameraNoise.m_FrequencyGain = _shakeFrequency;

                _shakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                _virtualCameraNoise.m_AmplitudeGain = 0f;
                _virtualCameraNoise.m_FrequencyGain = 0f;
                _shakeElapsedTime = 0f;
            }
        }
    }

    /// <summary>
    /// Triggers the screenshake.
    /// </summary>
    public void TriggerShake()
    {
        _shakeElapsedTime = _shakeDuration;
    }
}

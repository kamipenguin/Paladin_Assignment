using UnityEngine;

public class Hazard : MonoBehaviour
{
    private LifeManager _lifeManager;
    private CameraShake _cameraShake;
    private enum HazardType { Spikes, Saw, Pit};
    [SerializeField]
    private HazardType _hazardType;

    [SerializeField]
    private float _sawTurningSpeed = 1f;

    private void Awake()
    {
        _lifeManager = FindObjectOfType<LifeManager>();
        _cameraShake = FindObjectOfType<CameraShake>();
    }

    private void Update()
    {
        // if the hazard is a saw, rotate the object.
        if (_hazardType == HazardType.Saw)
            gameObject.transform.Rotate(new Vector3(0, 0, _sawTurningSpeed));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            _cameraShake.TriggerShake();
            StartCoroutine(_lifeManager.RespawnPlayer());
            _lifeManager.UpdateHearts();
        }
    }
}

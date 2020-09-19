using UnityEngine;

public class Hazard : MonoBehaviour
{
    private LifeManager _lifeManager;
    private enum HazardType { Spikes, Saw, Pit};
    [SerializeField]
    private HazardType _hazardType;

    [SerializeField]
    private float _sawTurningSpeed = 1f;

    private void Awake()
    {
        _lifeManager = FindObjectOfType<LifeManager>();
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
            _lifeManager.UpdateHearts();
            _lifeManager.RespawnPlayer();
        }
    }
}

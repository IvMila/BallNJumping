using UnityEngine;

public class SpawnRandomPositionSphere : MonoBehaviour
{
    [SerializeField] private GameObject _prefabSphere;
    [HideInInspector] public GameObject _newPrefab;
    private Vector3 _startPositionY;

    private void Awake()
    {
        RandomSpawn();
    }
    
    private void RandomSpawn()
    {
        _startPositionY = RandomSpawnPipe.EndPosY;

        float offset = ConstantsScript.DISTANCE_SPHERE_SPAWN  + _startPositionY.y;

        Vector3 newPosition = new Vector3(0, offset, -1.76f);

        _newPrefab = Instantiate(_prefabSphere, newPosition, Quaternion.identity);
    }
}

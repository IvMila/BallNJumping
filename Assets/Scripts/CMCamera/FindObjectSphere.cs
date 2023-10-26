using UnityEngine;
using Cinemachine;
public class FindObjectSphere : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachine;
    private SpawnRandomPositionSphere _spawnSphere;

    private void Start()
    {
        _spawnSphere = FindObjectOfType<SpawnRandomPositionSphere>();
        _cinemachine = GetComponent<CinemachineVirtualCamera>();

        _cinemachine.Follow = _spawnSphere._newPrefab.transform;
    }
}

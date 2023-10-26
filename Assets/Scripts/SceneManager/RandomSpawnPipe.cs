using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnPipe : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabsPipe;
    [SerializeField] private List<GameObject> _childPipe;
    private Vector3 _startPosY;
    public static Vector3 EndPosY;
    public static int RandomPipeSpawnCount;
    private int _randomIndexPipe;
    [SerializeField] private SpawnRandomPositionSphere _spherePositionY;

    public List<Color32> СolorList = new List<Color32>();

    private void Awake()
    {
        _startPosY = new Vector3(0, ConstantsScript.DISTANCE_PIPE_SPAWN, 0);

        Spawn();
    }

    private void Start()
    {
        ManagerEvents.OnRemoveTorus += RemoveTorusCount;
    }

    private void OnDestroy()
    {
        ManagerEvents.OnRemoveTorus -= RemoveTorusCount;
    }


    private void Spawn()
    {
        RandomPipeSpawnCount = Random.Range(15, 50);

        var randomColorIndexPipe = Random.Range(0, СolorList.Count);

        Color32 newColor = СolorList[randomColorIndexPipe];

        for (int i = 0; i < RandomPipeSpawnCount; i++)
        {
            float offset = (ConstantsScript.DISTANCE_PIPE_SPAWN * i) + _startPosY.y;
            Vector3 randomPosition = new Vector3(0, offset, 0);

            Quaternion rotation = Random.rotation;

            rotation.x = 0;
            rotation.z = 0;

             _randomIndexPipe = Random.Range(0, _prefabsPipe.Count);
            GameObject newTorus = Instantiate(_prefabsPipe[_randomIndexPipe], randomPosition, rotation, transform);
            _childPipe.Add(newTorus);
            _randomIndexPipe--;

            newTorus.GetComponent<Renderer>().material.color = newColor;

            EndPosY = new Vector3(0, newTorus.transform.position.y, 0);
        }
        EndPosY = new Vector3(0, EndPosY.y + ConstantsScript.DISTANCE_PIPE_SPAWN,0);
        Quaternion rotateFiirstPipe = Quaternion.Euler(0f, 90f, 0f);
        GameObject first = Instantiate(_prefabsPipe[0], EndPosY, rotateFiirstPipe, transform);
        _childPipe.Add(first);
        first.GetComponent<Renderer>().material.color = newColor;
    }

    private void RemoveTorusCount()
    {
        if (EndPosY.y > _spherePositionY._newPrefab.transform.position.y)
        {
            int lastIndex = _childPipe.Count - 1;

            GameObject removeTorus = _childPipe[lastIndex];

            _childPipe.Remove(removeTorus);
            Destroy(removeTorus);
        }
    }
}

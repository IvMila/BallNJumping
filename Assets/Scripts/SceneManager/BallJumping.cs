using UnityEngine;

public class BallJumping : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float _force;
    [SerializeField] private GameObject _spotPrefab;
    public static bool _gameLoss;
    public static bool _gameWin;

    private void Start()
    {
        _gameLoss = false;
        _gameWin = false;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = ConstantsScript.MAX_ANGULAR_VELOCITY_SPEED;
    }

    private void Update()
    {
        if (_rigidbody.velocity.magnitude >= _rigidbody.maxAngularVelocity)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _force;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(ConstantsScript.TAG_GROUND))
        {
            _rigidbody.velocity = Vector3.up * _force;

            Quaternion randomRotation = Random.rotation;
            randomRotation.x = 90;
            randomRotation.y = 0;

            Quaternion newRot = Quaternion.Euler(randomRotation.x, randomRotation.y, randomRotation.z * 120f);

            GameObject newSpot = Instantiate(_spotPrefab, new Vector3(transform.position.x, collision.transform.position.y + 0.325f, transform.position.z), newRot, collision.transform);
            Destroy(newSpot, 3f);
        }

        if (collision.gameObject.CompareTag(ConstantsScript.TAG_FINISH_GAME))
        {
            _gameWin = true;
            ManagerEvents.SetGameOver();
            Debug.Log("win");
        }


        if (collision.gameObject.CompareTag(ConstantsScript.TAG_OBSTACLE))
        {
            _gameLoss = true;
            ManagerEvents.SetGameOver();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(ConstantsScript.TAG_SCORE))
        {
            UIController._currentScore += 30;
            ManagerEvents.SetRemoveTorus();
        }
    }

    public void DontJumping()
    {
        _force = 0;
    }
}


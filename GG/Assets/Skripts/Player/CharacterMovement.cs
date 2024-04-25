using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _collisionMask;
    [Header("Character components")]
    [SerializeField] private Rigidbody _rigidbody;

    [Header("RaycastSettings")]
    [SerializeField,Min(0f)] private float distance = Mathf.Infinity;
    [SerializeField] private LayerMask _rayCastMask;
    [SerializeField] private Transform _point;
    [SerializeField] private Transform _aimPosition;

    [Header("Camera")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _cameraCenter;

    [Header("Character movement stats")]
    [HideInInspector] public float moveSpeed;
    [Min(0)] public float maxSpeed;
    [Min(0)] public float minSpeed;

    private void Start()
    {
        moveSpeed = maxSpeed;
    }

    public void Move(Vector3 moveDirection)
    {
        Vector3 _offset = Vector3.ClampMagnitude(moveDirection,1f) * moveSpeed * Time.deltaTime;

        _rigidbody.MovePosition(_rigidbody.position + _offset);
    }

    public void MouseRotate()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, distance, _rayCastMask))
        {
            Vector3 targetVector = hitInfo.point - transform.position;
            targetVector.Normalize();

            Quaternion newRotation = Quaternion.LookRotation(new Vector3(targetVector.x,0f,targetVector.z));
            _rigidbody.MoveRotation(newRotation);

            _aimPosition.position = Vector3.Lerp(_aimPosition.position, hitInfo.point, 25 * Time.deltaTime);
        }
    }

    public Vector3 RotateBody()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 _inputDirection = new Vector3(horizontal, 0, vertical);
        Vector3 _cameraForward = _cameraCenter.transform.forward;
        Vector3 _cameraRight = _cameraCenter.transform.right;

        _cameraForward.y = 0f;
        _cameraRight.y = 0f;

        Vector3 desiredDirection = _cameraForward * _inputDirection.z + _cameraRight * _inputDirection.x;
        Vector3 _movementDir = new Vector3(desiredDirection.x, 0f, desiredDirection.z);

        return _movementDir;
    }
}

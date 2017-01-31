using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpiderMotor))]
public class SpiderController : MonoBehaviour {
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float lookSensitivity = 3f;
    
    private SpiderMotor motor;
    private Animator animator;

    private void Start()
    {
        motor = GetComponent<SpiderMotor>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {

        #region calcaulte normal movement
        float _xMovement = Input.GetAxis("Horizontal");
        float _zMovement = Input.GetAxis("Vertical");

        Vector3 _movHorizontal = transform.right * _xMovement;
        Vector3 _movVertical = transform.forward * _zMovement;

        Vector3 _velocity = (_movHorizontal + _movVertical) * speed;
        animator.SetFloat("FowardVelocity", _zMovement);

        motor.Move(_velocity);
        #endregion

        #region calculate rotation fo the object
        float _yRotate = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRotate, 0f) * lookSensitivity;


        motor.Rotate(_rotation);
        #endregion

        #region Calcualte camera rotation 
        float _xRotation = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xRotation, 0f, 0f) * lookSensitivity;
        motor.RotateCamera(_cameraRotation);

        #endregion
    }


}

using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    /*
     *  Makes the field underneath to be visualized by the Unity Game engien as a public variable and be added to the menu without being public and is viewed as private by the script
     */
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;


    private PlayerMotor motor;
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        #region Calculate Player basic movements
        // Calculate movement velocity as a 3D vector
        #region Player movement
        float _xMovement = Input.GetAxis("Horizontal");
        float _zMovement = Input.GetAxis("Vertical");

        Vector3 _movHorizontal = transform.right * _xMovement;
        Vector3 _movVertical = transform.forward * _zMovement;

        //Final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;
        //Apply movemnt
        motor.Move(_velocity);
        #endregion

        #region Player Rotation (Y Axis)
        //Calculate rotaion as a 3D vector(turning around)
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f, _yRotation, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);
        #endregion

        #region Camera Rotation (X Axis)
        //Calculate camera rotaion as a 3D vector(turning around)
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        Vector3 _camerarotation = new Vector3(_xRotation, 0f, 0f) * lookSensitivity;

        //Apply camera rotation
        motor.RotateCamera(_camerarotation);
        #endregion



        #endregion

    }






}

using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    /*
     *  Makes the field underneath to be visualized by the Unity Game engien as a public variable and be added to the menu without being public and is viewed as private by the script
     */
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float thrusterForce = 1000f;



    



    [Header("Spring settings:")]
    //[SerializeField]
    //private JointDriveMode jointMode;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;


    //Component caching
    private PlayerMotor motor;
    private ConfigurableJoint joint;
    private Animator animator;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        animator = GetComponent<Animator>();
        SetJointSettings(jointSpring);
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
        Vector3 _velocity = (_movHorizontal + _movVertical) * speed;

        // Animate movement
        animator.SetFloat("FowardVelocity", _zMovement);


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
        float _camerarotation = _xRotation * lookSensitivity;

        //Apply camera rotation
        motor.RotateCamera(_camerarotation);
        #endregion

        #region Calculate thrusterForce(jump)
        Vector3 _thrusterForce = Vector3.zero;
        //Apple the thrusterForce
        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f); // when jumping and holding make the  spring to 0f making the object nto fall back to the ground
        }else
        {
            SetJointSettings(jointSpring);
        }
        motor.ApplyThruster(_thrusterForce);
        #endregion
        #endregion


    }
    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive {
            positionSpring = _jointSpring ,
            maximumForce = jointMaxForce
        };
    }






}

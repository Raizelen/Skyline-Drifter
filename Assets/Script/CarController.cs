using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody F8;
    public wheelColliders colliders;
    public wheelMeshes wheelMeshes;
    public float gasInput;
    public float steerInput;

    public float speed;
    [SerializeField]public float motorPower;
    public AnimationCurve steeringCurve;



    // Start is called before the first frame update
    void Start()
    {
        F8 = gameObject.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        speed = F8.velocity.magnitude;
        CheckInput();
        engine();
        applySteering();
        UpdateWheelPositions();
    }

    void CheckInput()
    {
        gasInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }
    void engine() // code to move car
    {
        colliders.RRWheel.motorTorque = motorPower * gasInput;
        colliders.RLWheel.motorTorque = motorPower * gasInput;
    }
    void applySteering()
    {
        float steeringAngle = steerInput * steeringCurve.Evaluate(speed); //for evaluating amount of with
        colliders.FRWheel.steerAngle = steeringAngle;                                                                  //respect to speed
    }
    void UpdateWheelPositions()
    {
        UpdateWheel(colliders.FRWheel, wheelMeshes.FRWheel);
        UpdateWheel(colliders.FLWheel, wheelMeshes.FLWheel);
        UpdateWheel(colliders.RRWheel, wheelMeshes.RRWheel);
        UpdateWheel(colliders.RLWheel, wheelMeshes.RLWheel);
    }
    void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh) //spin wheels
    {
        Quaternion quat;
        Vector3 position;
        coll.GetWorldPose(out position, out quat);
        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = quat;

    }
}

[System.Serializable]
public class wheelColliders
{
    public WheelCollider FRWheel;
    public WheelCollider FLWheel;
    public WheelCollider RRWheel;
    public WheelCollider RLWheel;
}
[System.Serializable]
public class wheelMeshes
{
    public MeshRenderer FRWheel;
    public MeshRenderer FLWheel;
    public MeshRenderer RRWheel;
    public MeshRenderer RLWheel;
}
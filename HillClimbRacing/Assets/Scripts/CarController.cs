using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] Rigidbody2D backTire;
    [SerializeField] Rigidbody2D frontTire;
    [SerializeField] Rigidbody2D carBody;
    [SerializeField] float carSpeed;
    [SerializeField] float carRotationValue;
    [SerializeField] float carFuelConsumption;
    [SerializeField] WheelJoint2D frontTireWheelJoint;
    [SerializeField] WheelJoint2D backTireWheelJoint;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] Image fuelImage;
    [SerializeField] CarPartSO carPartSO;


    public Vector3 carPos;
    public Vector3 startPos;
    public Vector3 endPos;

    private float horizontalMove;
    public static float fuel = 1;
    public static float distance = 0;
    public TextMeshProUGUI distanceText;

    //---------------------------------------------//
    [Header("Car Boost Settings")]
    public float speedBoostDuration = 3f;
    public float decreaseDuration = 1.0f;

    public float speedBoostMultiplier = 2f;
    public float startBoostMultiplier = 1.5f;
    public float endBoostMultiplier = 1.0f;

    public float currentSpeed;
    private bool isSpeedBoosted;
    private bool slowCar;
    private bool carStop;
    //---------------------------------------------//
    float torqueValue;

    private void Start()
    {
        startPos = GameObject.Find("Start Car Position").GetComponent<Transform>().position;
        endPos = GameObject.Find("Finish Flag Position").GetComponent<Transform>().position;
        fuelImage = GameObject.Find("Fuel Value").GetComponent<Image>();
        fuelImage.fillAmount = fuel;
        distanceText = GameObject.Find("Distance Text").GetComponent<TextMeshProUGUI>();
        carStop = false;

        currentSpeed = carPartSO.speed;
        isSpeedBoosted = false;
        SetSuspension();

    }
    private void FixedUpdate()
    {
        CarMovement();
    }

    public void CarMovement()
    {
        carPos = transform.position;
        horizontalMove = Input.GetAxis("Horizontal");

        if (IsGround() && fuel > 0)
        {
            currentSpeed = Mathf.Clamp(currentSpeed, 0, carPartSO.maxSpeed);
            torqueValue = -horizontalMove * currentSpeed * Time.deltaTime;
            backTire.AddTorque(torqueValue);
            frontTire.AddTorque(torqueValue);
            carBody.AddTorque(torqueValue);;
        }
        else if (!IsGround())
        {
            float rotationValue = horizontalMove * carPartSO.rotationValue;
            transform.Rotate(0, 0, rotationValue);
        }
        else if (fuel <= 0)
        {
            carStop = true;
            CarSlow();
        }

        if (slowCar)
        {
            CarSlow();
        }

        SetFuel();
        SetDistance();
        Horn();
    }

    public bool IsGround()
    {
        if (Physics2D.OverlapCircle(backTire.position, 0.5f, groundLayerMask) || Physics2D.OverlapCircle(frontTire.position, 0.5f, groundLayerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetFuel()
    {
        fuel -= carPartSO.fuelConsumption * Time.deltaTime * Mathf.Abs(horizontalMove);
        fuelImage.fillAmount = fuel;
    }

    public void SetDistance()
    {
        distance = (int)Vector3.Distance(carPos, startPos);
        distanceText.text = distance.ToString() + "m / " + endPos.x + "m";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boost"))
        {
            isSpeedBoosted = true;
            SoundManager.instance.PlayWithIndex(6);
            currentSpeed *= speedBoostMultiplier;
            Destroy(collision.gameObject);
            slowCar = true;
        }
    }

    public void CarSlow()
    {
        float speed = Time.deltaTime * speedBoostMultiplier * 10f;
        if (carStop && currentSpeed >= 0)
        {
            currentSpeed -= speed;
            if (currentSpeed <= 0)
            {
                currentSpeed = 0;
                carStop = false;
            }

        }
        if (slowCar && isSpeedBoosted)
        {
            currentSpeed -= speed;
            if (currentSpeed <= carPartSO.speed)
            {
                currentSpeed = carPartSO.speed;
                slowCar = false;
                isSpeedBoosted = false;
            }
        }
    }

    public void SetSuspension()
    {

        // bTS = backTireSuspension, fTS = frontTireSuspension
        JointSuspension2D bTS = backTireWheelJoint.suspension;
        JointSuspension2D fTS = frontTireWheelJoint.suspension;

        bTS.dampingRatio = carPartSO.dampingValue;
        bTS.frequency = carPartSO.frequencyValue;
        backTireWheelJoint.suspension = bTS;

        fTS.dampingRatio = carPartSO.dampingValue;
        fTS.frequency = carPartSO.frequencyValue;
        frontTireWheelJoint.suspension = fTS;
    }

    public void Horn()
    {
        if (Input.GetKey(KeyCode.H))
        {
            SoundManager.instance.PlayWithIndex(5);
        }
    }
}

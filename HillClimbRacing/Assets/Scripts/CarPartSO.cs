using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car Parts", menuName = "Car Parts")]
public class CarPartSO : ScriptableObject
{
    [Header("Car Settings")]
    public float speed;
    public float maxSpeed;
    public float dampingValue; // suspension esneklik
    public float frequencyValue; // suspension sabitleme
    public float fuelConsumption;
    public float rotationValue;
    
    

    [Header("Upgrade Levels")]
    public int engineLevel;
    public int suspensionLevel;
    public int tiresLevel;
    public int airControlLevel;

    public int[] levels;

    private void OnEnable()
    {
        levels = new int[] { engineLevel, suspensionLevel, tiresLevel, airControlLevel };
    }
}

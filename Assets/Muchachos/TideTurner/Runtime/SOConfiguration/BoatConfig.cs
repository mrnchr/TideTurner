using UnityEngine;

[CreateAssetMenu(menuName = "BoatConfig", fileName = "BoatConfig")]
public class BoatConfig : ScriptableObject
{
    [Range(30, 180)] public int DeathAngle = 80;

    [Range(1, 2)] public float DeathHeight = 1.4f;

    [Range(2, 10)] public float DeathGravity = 5;

    [Range(0, 15)]public float DeathForce = 2.77f;
}
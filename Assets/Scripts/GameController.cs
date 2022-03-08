using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public GameObject rocket;
    public float force = 2f;
    public float angularForce = 15f;
    public GameObject rocketEngineAudio;
    public ParticleSystem rocketPlume;

    private Rigidbody2D rocketRb;
    private EngineAudio engineAudio;
    private bool engineOn;
    private bool rotateLeftOn;
    private bool rotateRightOn;

    private void Start()
    {
        rocketRb = rocket.GetComponent<Rigidbody2D>();
        engineAudio = rocketEngineAudio.GetComponent<EngineAudio>();
    }

    private void FixedUpdate()
    {
        if (engineOn)
        {
            Transform rocketTransform = rocket.GetComponent<Transform>();
            Vector3 direction = rocketTransform.up;
            Vector2 forceDirection = new Vector2(direction.x, direction.y);
            rocketRb.AddForce(forceDirection * force);
        }

        if (rotateLeftOn)
        {
            AddTorqueImpulse(angularForce);
        }

        if (rotateRightOn)
        {
            AddTorqueImpulse(angularForce * -1);
        }
    }

    public void Thrust()
    {
        engineOn = true;
        engineAudio.engineOn = true;
        rocketPlume.Play();
    }

    public void Release()
    {
        engineOn = false;
        engineAudio.engineOn = false;
        rocketPlume.Stop();
    }

    public void RotateRight()
    {
        rotateRightOn = true;
    }
    public void ReleaseRotateRight()
    {
        rotateRightOn = false;
    }

    public void RotateLeft()
    {
        rotateLeftOn = true;
    }

    public void ReleaseRotateLeft()
    {
        rotateLeftOn = false;
    }

    private void AddTorqueImpulse(float angularChangeInDegrees)
    {
        var impulse = (angularChangeInDegrees * Mathf.Deg2Rad) * rocketRb.inertia;

        rocketRb.AddTorque(impulse, ForceMode2D.Impulse);
    }
}

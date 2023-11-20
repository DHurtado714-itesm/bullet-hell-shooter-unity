using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 1f;
    public float rotation = 0f;
    public float speed = 1f;
    public BulletPattern pattern;

    private Vector3 spawnPoint;
    private float timer = 0f;
    private bool isCurrentlyVisible = false;

    void Start()
    {
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLife) 
        {
            if (isCurrentlyVisible)
            {
                BulletCounter.Instance.RemoveVisibleBullet(this);
            }
            Destroy(gameObject);
        }

        timer += Time.deltaTime;

        switch (pattern)
        {
            case BulletPattern.Straight:
                transform.position = StraightMovement(timer);
                break;
            case BulletPattern.MinusFortyFive:
                transform.position = FortyFiveDegreeMovement(timer, 110f);
                break;
            case BulletPattern.PlusFortyFive:
                transform.position = FortyFiveDegreeMovement(timer, 70f);
                break;
            case BulletPattern.Sinusoidal:
                transform.position = SinusoidalMovement(timer);
                break;
        }

        UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        bool isVisible = IsVisibleFrom(Camera.main);
        if (BulletCounter.Instance != null)
        {
            if (isVisible && !isCurrentlyVisible)
            {
                // La bala acaba de volverse visible
                BulletCounter.Instance.AddVisibleBullet(this);
                isCurrentlyVisible = true;
            }
            else if (!isVisible && isCurrentlyVisible)
            {
                // La bala acaba de volverse no visible
                BulletCounter.Instance.RemoveVisibleBullet(this);
                isCurrentlyVisible = false;
            }
        }
    }

    private bool IsVisibleFrom(Camera camera)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, new Bounds(transform.position, Vector3.one * 0.1f)); // Ajusta el tamaño si es necesario
    }

    private Vector3 StraightMovement(float timer)
    {
        float z = timer * speed;
        return spawnPoint + new Vector3(0, 0, -z);
    }

    private Vector3 SinusoidalMovement(float timer)
    {
        float amplitude = 1f;
        float frequency = 2f;
        float x = amplitude * Mathf.Sin(frequency * timer);
        float z = timer * speed;
        return spawnPoint + new Vector3(x, 0, -z);
    }

    private Vector3 FortyFiveDegreeMovement(float timer, float angle)
    {
        float radianAngle = angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(radianAngle) * timer * speed;
        float z = Mathf.Sin(radianAngle) * timer * speed;
        return spawnPoint + new Vector3(x, 0, -z);
    }
}

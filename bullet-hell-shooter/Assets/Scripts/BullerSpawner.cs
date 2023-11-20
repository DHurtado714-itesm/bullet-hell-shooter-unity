using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletPattern { Straight, MinusFortyFive, PlusFortyFive, Sinusoidal }
public class BulletSpawner : MonoBehaviour
{

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;

    [Header("Spawner Attributes")]
    [SerializeField] private float firingRate = 1f;

    private GameObject spawnedBullet;
    private float timer = 0f;
    private float patternChangeTimer = 0f;
    private BulletPattern currentPattern = BulletPattern.Straight;
    private BulletPattern[] availablePatterns = {BulletPattern.Straight, BulletPattern.MinusFortyFive, BulletPattern.PlusFortyFive, BulletPattern.Sinusoidal};

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= firingRate)
        {
            Fire();
            timer = 0;
        }
        patternChangeTimer += Time.deltaTime;
        if(patternChangeTimer >= 10f)
        {
            ChangePattern();
            patternChangeTimer = 0;
        }
    }

    private void Fire()
    {
        if(bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            spawnedBullet.GetComponent<Bullet>().pattern = currentPattern;
        }
    }

    private void ChangePattern()
    {
        currentPattern = (BulletPattern)(((int)currentPattern + 1) % System.Enum.GetValues(typeof(BulletPattern)).Length);
    }
}

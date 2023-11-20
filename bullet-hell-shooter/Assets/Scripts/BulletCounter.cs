using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletCounter : MonoBehaviour
{
    public static BulletCounter Instance;
    private HashSet<Bullet> visibleBullets = new HashSet<Bullet>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddVisibleBullet(Bullet bullet)
    {
        visibleBullets.Add(bullet);
    }

    public void RemoveVisibleBullet(Bullet bullet)
    {
        visibleBullets.Remove(bullet);
    }

    public int GetVisibleBulletCount()
    {
        return visibleBullets.Count;
    }
}

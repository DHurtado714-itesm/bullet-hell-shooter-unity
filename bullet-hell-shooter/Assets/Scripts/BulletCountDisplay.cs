using UnityEngine;
using TMPro;

public class BulletCountDisplay : MonoBehaviour
{
    public TextMeshPro bulletCountText;

    void Update()
    {
        if (bulletCountText != null && BulletCounter.Instance != null)
        {
            bulletCountText.text = "Sushis: " + BulletCounter.Instance.GetVisibleBulletCount();
        }
    }
}

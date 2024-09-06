using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalicBehaviour : MeeleWeaponBehaviour
{
    public float dotInterval = 0.5f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            // Bắt đầu gây sát thương theo thời gian khi kẻ thù vào phạm vi
            StartCoroutine(DamageOverTime(enemyStats, collision));
        }
    }

    // Coroutine để gây sát thương theo thời gian nếu kẻ thù vẫn trong phạm vi
    IEnumerator DamageOverTime(EnemyStats enemyStats, Collider2D collision)
    {
        while (collision != null && collision.CompareTag("Enemy"))
        {
            // Gây sát thương nếu kẻ thù còn trong phạm vi collider của Galic
            enemyStats.TakeDamage(currentDamage);

            // Đợi cho tới lần tiếp theo gây sát thương
            yield return new WaitForSeconds(dotInterval);

            // Kiểm tra nếu kẻ thù vẫn trong collider
            if (collision == null || !collision.bounds.Intersects(GetComponent<Collider2D>().bounds))
            {
                // Dừng gây sát thương nếu kẻ thù đã rời phạm vi
                yield break;
            }
        }
    }

    // Phương thức OnTriggerExit2D để đảm bảo khi kẻ thù rời phạm vi thì dừng sát thương
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Dừng coroutine nếu kẻ thù rời khỏi collider
            StopAllCoroutines();
        }
    }
}

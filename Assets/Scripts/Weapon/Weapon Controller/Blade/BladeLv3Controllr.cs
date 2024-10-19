using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class BladeLv3Controllr : MeeleSkillBehaviour
{
    AudioManager audioManager;
    private bool isFinish = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
            audioManager.PlaySFX(audioManager.bladeMaxAttack);
            base.Attack();

            GetComponent<MeeleSkillBehaviour>().DirectionChecker(playerMove.lastMoveDirection);

            // Tấn công lần thứ hai sau một khoảng thời gian ngắn, ở hướng ngược lại
            StartCoroutine(PerformSecondAttack());
    }

    // Coroutine để thực hiện đòn tấn công thứ hai ở hướng ngược lại
    private IEnumerator PerformSecondAttack()
    {
        // Đợi một khoảng thời gian trước khi tấn công lần hai (ví dụ: 0.2 giây)
        yield return new WaitForSeconds(0.3f);

        // Đảo ngược hướng của lần tấn công thứ hai
        Vector3 reversedDirection = new Vector3(-playerMove.lastMoveDirection.x, playerMove.lastMoveDirection.y);

        GetComponent<MeeleSkillBehaviour>().DirectionChecker(reversedDirection);
    }

    protected override void FinishAttack()
    {
        if (skillData.Level >= 3 && !isFinish)
        {
            isFinish = true;
        }
        else
        {
            base.FinishAttack();
            isFinish = false;
        }
    }
}

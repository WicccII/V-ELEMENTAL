using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearLv3Controller : SkillController
{
    // Start is called before the first frame update
// Overloaded Attack method with a delay in the loop
    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(SpawnSpearsWithDelay());
    }

    // Coroutine to handle delay between spear spawns
    private IEnumerator SpawnSpearsWithDelay()
    {
        float randomNumber;
        for (int i = 0; i < skillData.Level - 1; i++)
        {
            randomNumber = Random.Range(-0.5f, 0.5f);
            GameObject spawnedSpear = Instantiate(skillData.Prefab);
            spawnedSpear.transform.position = transform.position + new Vector3(randomNumber, randomNumber, 0); // Weapon spawns at player's position
            spawnedSpear.GetComponent<SpearBehaviour>().DirectionChecker(playerMove.lastMoveDirection); // Set direction where weapon will go

            // Add delay before spawning the next spear
            yield return new WaitForSeconds(0.1f); // Adjust delay as necessary
        }
    }
}

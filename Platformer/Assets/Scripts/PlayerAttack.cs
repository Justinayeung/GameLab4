using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Variables")]
    public float startTimeBtwAttack;
    public float attackRange;
    public int damage;

    [Header("References")]
    public Transform attackPos;
    public LayerMask isEnemy;

    float timeBtwAttack;

    void Update() {
        if(timeBtwAttack <= 0) {
            if(Input.GetKey(KeyCode.X)) {
                //Camera shake
                //Player attack animation
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, isEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++) {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
                timeBtwAttack = startTimeBtwAttack;
            }  
        } else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}

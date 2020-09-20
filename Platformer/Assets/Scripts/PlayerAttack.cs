using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Variables")]
    public float startTimeBtwAttack;
    public float attackRange;
    public int damage;
    public float duration = 0.15f;
    public float magnitude = 0.4f;

    [Header("References")]
    public Transform attackPos;
    public LayerMask isEnemy;
    public Animator anim;
    public CameraShake cameraShake;

    float timeBtwAttack;

    void Update() {
        if(timeBtwAttack <= 0) {
            if(Input.GetKeyDown(KeyCode.X)) {
                StartCoroutine(cameraShake.Shake(duration, magnitude));
                timeBtwAttack = startTimeBtwAttack;
                anim.SetTrigger("Attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, isEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++) {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
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

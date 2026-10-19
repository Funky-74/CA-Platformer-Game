using UnityEngine;
using UnityEngine.InputSystem;

public class Mele : MonoBehaviour
{

    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float damageAmount = 1.5f;

    private RaycastHit2D[] hits;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputSystem.actions["Attack2"].WasPressedThisFrame())
        {
            Attack2();
            anim.SetTrigger("Mele");
        }
    }

    private void Attack2()
    {
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, enemyLayer);

        for (int i=0; i < hits.Length; i++)
        {
            Enemy enemy = hits[i].collider.GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.TakeDamage(damageAmount);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackTransform == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
}

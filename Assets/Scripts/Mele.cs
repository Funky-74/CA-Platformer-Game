using UnityEngine;
using UnityEngine.InputSystem;
public class Mele : MonoBehaviour
{

    //tutorial followed - https://www.youtube.com/watch?v=oGW9UQQh-6w


    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float damageAmount = 1.5f;
    private AudioSource audio;
    [SerializeField] private AudioClip meleClip;

    private RaycastHit2D[] hits;

    private Animator anim;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputSystem.actions["Attack2"].WasPressedThisFrame())
        {
            Attack2();
            anim.SetTrigger("Mele");
            audio.PlayOneShot(meleClip, 2f);
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
        //draws red sphere to show area affected by mele
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
}

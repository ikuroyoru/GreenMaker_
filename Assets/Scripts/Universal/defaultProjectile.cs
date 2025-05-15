using UnityEngine;

public class defaultProjectile : MonoBehaviour
{
    private float speed;
    private float lifetime;
    private float damage;
    private float timer;
    private Vector2 moveDirection;

    public void SetValues(float _speed, float _lifetime, float _damage, Vector2 _direction)
    {
        speed = _speed;
        lifetime = _lifetime;
        damage = _damage;
        moveDirection = _direction;
        timer = lifetime;
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        timer -= Time.deltaTime;
        if (timer <= 0f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            hp bossHP = other.GetComponentInChildren<hp>();
            if (bossHP != null) bossHP.takeDamage(damage);
            Destroy(gameObject);
        }
    }
}

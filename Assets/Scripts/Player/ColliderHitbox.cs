using UnityEngine;
using System.Collections;

/// <summary>
/// esse script ira gerenciar um collisor frente ao personagem, onde será ativado sempre que atacar 
/// </summary>
public class ColliderHitbox : MonoBehaviour
{
    public float knockbackForce = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Calcula a direção do knockback (da posição do jogador para a do inimigo)
            Vector3 knockbackDir = (other.transform.position - transform.position).normalized;
            knockbackDir.y = 0; //evitar do inimigo entrar debaixo do terreno
            Vector3 knockbackTarget = other.transform.position + (knockbackDir * knockbackForce);

            StartCoroutine(KnockbackEnemy(other.transform, knockbackTarget, 0.15f));
        }
    
    }

    /// <summary>
    /// Aplica um knockback suave ao inimigo movendo-o diretamente pelo transform.
    /// </summary>
    /// <param name="enemy">O transform do inimigo.</param>
    /// <param name="targetPos">A posição final do knockback.</param>
    /// <param name="duration">O tempo que o knockback deve durar.</param>
    private IEnumerator KnockbackEnemy(Transform enemy, Vector3 targetPos, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startPos = enemy.position;

        while (elapsedTime < duration)
        {
            // Move o inimigo gradualmente da posição inicial para a posição final
            enemy.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        enemy.position = targetPos;
    }
}

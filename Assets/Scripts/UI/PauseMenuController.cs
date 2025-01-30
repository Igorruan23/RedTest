using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;
    private Vector3 targetScale = Vector3.zero;
    private float scaleSpeed = 10f;

    private void Start()
    {
        pauseMenu.transform.localScale = Vector3.zero;
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        pauseMenu.transform.localScale = Vector3.Lerp(pauseMenu.transform.localScale, targetScale, Time.unscaledDeltaTime * scaleSpeed);
    }

    /// <summary>
    /// Método chamado pelo Input System quando o jogador aperta o botão de pausa.
    /// </summary>
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TogglePauseMenu();
        }
    }


    /// <summary>
    /// Ativa ou desativa o menu de pausa.
    /// </summary>
    private void TogglePauseMenu()
    {
        isPaused = !isPaused;
        targetScale = isPaused ? Vector3.one : Vector3.zero;
        pauseMenu.SetActive(true);

        if (!isPaused)
        {
            StartCoroutine(HideAfterAnimation());
        }
    }

    /// <summary>
    /// Aguarda o menu encolher antes de desativá-lo.
    /// </summary>
    IEnumerator HideAfterAnimation()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        if (!isPaused) pauseMenu.SetActive(false);
    }
}

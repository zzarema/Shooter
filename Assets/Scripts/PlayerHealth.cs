using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player & Components")]
    [SerializeField] private GameObject thePlayer;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject gun; 

    [Header("UI & Effects")]
    [SerializeField] private GameObject canvasGameOver;
    [SerializeField] private Image blackScreen;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private float fadeSpeed = 1.5f;
    [SerializeField] private float delayBeforeGameOver = 2f;

    private AudioSource audioSource;
    private bool isTriggered = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;

        if (other.CompareTag("Enemy"))
        {
            isTriggered = true;

         
            if (thePlayer.TryGetComponent(out PlayerMovement moveScript))
                moveScript.enabled = false;

            if (thePlayer.TryGetComponent(out CharacterController controller))
                controller.enabled = false;

           
            if (playerAnimator != null)
                playerAnimator.Play("Die");

           
            if (gun != null)
                gun.SetActive(false);

            
            if (loseSound != null)
                audioSource.PlayOneShot(loseSound);

         
            StartCoroutine(FadeToBlackThenShowGameOver());
        }
    }

    IEnumerator FadeToBlackThenShowGameOver()
    {
        float alpha = 0f;

        if (blackScreen != null)
        {
            while (alpha < 1f)
            {
                alpha += Time.deltaTime * fadeSpeed;
                blackScreen.color = new Color(0f, 0f, 0f, alpha);
                yield return null;
            }
        }

        yield return new WaitForSeconds(delayBeforeGameOver);

        if (canvasGameOver != null)
            canvasGameOver.SetActive(true);

        Time.timeScale = 0f;
    }
}

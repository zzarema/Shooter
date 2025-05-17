using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    public GameObject winCanvas;            
    public AudioClip winMusic;            
    public GameObject thePlayer;           
    public Animator playerAnimator;       
    public GameObject fireworks;          
    public float delayBeforeWinCanvas = 2f; 

    private bool hasWon = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasWon) return;

        if (other.CompareTag("Player") && ScoreManager.instance.GetScore() >= 6)
        {
            hasWon = true;

          

            
            if (thePlayer.TryGetComponent(out PlayerMovement moveScript))
                moveScript.enabled = false;

            if (thePlayer.TryGetComponent(out CharacterController controller))
                controller.enabled = false;

           
            if (playerAnimator != null)
                playerAnimator.SetTrigger("Win");

           
            PlayWinMusic();

           
            StartCoroutine(ShowWinCanvasDelayed());
        }
    }

    IEnumerator ShowWinCanvasDelayed()
    {
        yield return new WaitForSeconds(delayBeforeWinCanvas);

        if (winCanvas != null)
            winCanvas.SetActive(true);

        
    }

    
    private void PlayWinMusic()
    {
        if (winMusic != null)
        {
            AudioSource audioSource = thePlayer.GetComponent<AudioSource>(); 
            if (audioSource == null)
                audioSource = thePlayer.AddComponent<AudioSource>(); 

            audioSource.PlayOneShot(winMusic);
        }
    }

    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

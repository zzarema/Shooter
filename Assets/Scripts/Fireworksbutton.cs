
using UnityEngine;

public class FireworksButton : MonoBehaviour
{
    public GameObject fireworks;

    public void PlayFireworks()
    {
        if (fireworks != null)
        {
           
            fireworks.SetActive(true);

       
            var ps = fireworks.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
                Invoke(nameof(StopFireworks), 5f); 
            }
            else
            {
                Debug.LogWarning("no ParticleSystem!");
            }
        }
    }

    private void StopFireworks()
    {
        if (fireworks != null)
        {
            fireworks.SetActive(false);
        }
    }
}

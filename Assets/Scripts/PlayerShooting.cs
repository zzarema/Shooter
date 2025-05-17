using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 30f;
    public float rayDistance = 100f;

    public AudioClip shootSound;
    private AudioSource audioSource; 

    void Start()
    {
       
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootVisual();
            RaycastShoot();
            PlayShootSound(); 
        }
    }

    void ShootVisual()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.forward * bulletSpeed;
            }
        }
    }

    void RaycastShoot()
    {
        Camera activeCam = GetActiveCamera();
        if (activeCam == null) return;

        Ray ray = activeCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);

                if (ScoreManager.instance != null)
                {
                    ScoreManager.instance.AddScore(1);
                }
            }
        }
    }

    void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    Camera GetActiveCamera()
    {
        if (Camera.main != null) return Camera.main;

        Camera[] allCameras = Camera.allCameras;

        foreach (Camera cam in allCameras)
        {
            if (cam.gameObject.activeInHierarchy) return cam;
        }

        return null;
    }
}

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // Ses kayna��
    public AudioClip walkClip; // Y�r�me sesi
    public AudioClip sprintClip; // Ko�ma sesi
    public AudioClip jumpClip; // Z�plama sesi
    public AudioClip slowWalkClip; // Yava� y�r�me sesi

    public float volume = 0.5f; // Ses d�zeyi
    public float walkSpeed = 1.0f; // Y�r�me sesinin �alma h�z�
    public float slowWalkSpeed = 0.5f; // Yava� y�r�me sesinin �alma h�z�
    public float sprintSpeed = 1.5f; // Ko�ma sesinin �alma h�z�

    private bool isWalking = false;
    private bool isRunning = false;
    private bool isSlowing = false;

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned in the Inspector");
        }
    }

    void Update()
    {
        // Y�r�me hareketi ba�lad���nda y�r�me sesini �alma
        if ((Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) && !isWalking)
        {
            PlaySound(walkClip, walkSpeed);
            isWalking = true;
        }

        // Y�r�me hareketi bitti�inde y�r�me sesini durdurma
        if ((Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0) && isWalking)
        {
            audioSource.Stop();
            isWalking = false;
        }

        // Ko�ma sesini �alma
        if (Input.GetKey(KeyCode.LeftShift) && !isRunning)
        {
            PlaySound(sprintClip, sprintSpeed);
            isRunning = true;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && isRunning)
        {
            audioSource.Stop();
            isRunning = false;
        }

        // Yava� y�r�me sesini �alma
        if (Input.GetKey(KeyCode.LeftControl) && !isSlowing)
        {
            PlaySound(slowWalkClip, slowWalkSpeed);
            isSlowing = true;
        }
        else if (!Input.GetKey(KeyCode.LeftControl) && isSlowing)
        {
            audioSource.Stop();
            isSlowing = false;
        }

        // Z�plama sesini �alma
        if (Input.GetButtonDown("Jump"))
        {
            PlaySound(jumpClip);
        }
    }

    void PlaySound(AudioClip clip, float pitch = 1.0f)
    {
        audioSource.Stop(); // �nceki sesi durdur

        // Ses d�zeyini ve �alma h�z�n� ayarla
        audioSource.volume = volume;
        audioSource.pitch = pitch;

        // Yeni sesi �al
        audioSource.clip = clip;
        audioSource.Play();
    }
}

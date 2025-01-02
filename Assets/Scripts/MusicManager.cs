using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [Header("Music Settings")]
    [SerializeField] private AudioSource audioSource;         // Nguồn phát âm thanh
    [SerializeField] private AudioClip[] musicClips;          // Danh sách các bài nhạc
    [SerializeField] private float volume = 0.5f;             // Âm lượng ban đầu
    [SerializeField] private bool loopPlaylist = true;        // Lặp lại danh sách nhạc

    private int currentTrackIndex = 0;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false; // Không lặp từng bài nhạc (lặp danh sách sẽ quản lý riêng).
        }

        audioSource.volume = volume;
        PlayNextTrack();
    }

    private void Update()
    {
        // Kiểm tra nếu bài nhạc hiện tại đã kết thúc
        if (!audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    public void PlayNextTrack()
    {
        if (musicClips.Length == 0) return;

        // Phát bài tiếp theo
        audioSource.clip = musicClips[currentTrackIndex];
        audioSource.Play();

        // Cập nhật bài tiếp theo trong danh sách
        currentTrackIndex++;
        if (currentTrackIndex >= musicClips.Length)
        {
            currentTrackIndex = loopPlaylist ? 0 : musicClips.Length - 1;
        }
    }

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume); // Đảm bảo âm lượng nằm trong khoảng [0, 1]
        audioSource.volume = volume;
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}

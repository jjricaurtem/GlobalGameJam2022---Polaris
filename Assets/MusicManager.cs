using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip mainClip;
    [SerializeField] private AudioClip deadClip;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            _audioSource.clip = mainClip;
            _audioSource.loop = true;
        }
        else if (GameManager.sharedInstance.currentGameState == GameState.Dying)
        {
            _audioSource.clip = deadClip;
            _audioSource.loop = false;
            _audioSource.Play();
        }
    }
}
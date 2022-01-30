using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objectToEnable;

    private CameraFollow _cameraFollow;

    // Start is called before the first frame update
    private void Start()
    {
        _cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void IntroCreatesPlayer()
    {
        foreach (var go in objectToEnable) go.SetActive(true);
        _cameraFollow.enabled = true;
        gameObject.SetActive(false);
        GameManager.sharedInstance.currentGameState = GameState.inTheGame;
    }
}
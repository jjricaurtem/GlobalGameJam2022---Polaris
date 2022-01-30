using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    [SerializeField] private Sprite[] backgroundList;
    private SpriteRenderer _spriteRenderer;
    private float _startParentPositionX;

    private float _startPositionX;

    // Start is called before the first frame update
    private void Start()
    {
        _startParentPositionX = transform.parent.position.x;
        _startPositionX = transform.localPosition.x;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = GetRandomBackground();
    }

    // Update is called once per frame
    private void Update()
    {
        var pos = transform.parent.position.x - _startParentPositionX;
        var offset = (int)(pos / 5);
        Debug.Log(offset);
        transform.localPosition = new Vector2(_startPositionX - offset / 100f, transform.localPosition.y);
    }

    private Sprite GetRandomBackground()
    {
        return backgroundList[Random.Range(0, backgroundList.Length)];
    }
}
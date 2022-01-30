using System;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    private float _startParentPositionX;

    private Camera _mainCamera;
    private float _startPositionX;

    // Start is called before the first frame update
    private void Start()
    {
        _mainCamera = Camera.main;
        _startParentPositionX = transform.parent.position.x;
        _startPositionX = transform.localPosition.x;
    }

    // Update is called once per frame
    private void Update()
    {
        var pos = transform.parent.position.x - _startParentPositionX;
        var offset = (int)(pos / 5);
        Debug.Log(offset);
        transform.localPosition = new Vector2(_startPositionX-(offset/100f), transform.localPosition.y);
    }
}
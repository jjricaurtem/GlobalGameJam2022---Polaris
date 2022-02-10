using UnityEngine;
using UnityEngine.UI;

public class ScoreRow : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text scoreText;


    public void Initialize(Document document)
    {
        nameText.text = document.fields.name.stringValue;
        scoreText.text = document.fields.score.stringValue;
    }

}
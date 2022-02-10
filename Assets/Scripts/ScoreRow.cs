using UnityEngine;
using UnityEngine.UI;

public class ScoreRow : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text scoreText;


    public void Initialize(Score score)
    {
        nameText.text = score.name.stringValue;
        scoreText.text = score.score.integerValue.ToString();
    }

}
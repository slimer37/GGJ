using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] dreamloLeaderBoard _dreamloLeaderBoard;
    [SerializeField] TMP_Text _leaderboard;
    [SerializeField] TMP_InputField _inputField;
    [SerializeField] GameObject _group;

    void Update()
    {
        if (!Health.IsDead) return;

        var scores = _dreamloLeaderBoard.ToListHighToLow();

        _leaderboard.text = "";

        foreach (var score in scores)
        {
            _leaderboard.text += score.playerName + " - " + score.score + '\n';
        }
    }

    public void AddScore()
    {
        Destroy(_group);
        _dreamloLeaderBoard.AddScore(_inputField.text, Mathf.FloorToInt(Score.Instance.Value));
    }
}

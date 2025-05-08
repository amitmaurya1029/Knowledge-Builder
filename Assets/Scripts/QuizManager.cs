using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers;
    public int correctAnswerIndex;
}

public class QuizManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI scoreText;
    public GameObject quizPanel;
    public GameObject endPanel;
    public TextMeshProUGUI finalScoreText;

    [Header("Quiz Data")]
    public Question[] questions;

    private int currentQuestionIndex = 0;
    private int score = 0;

    void Start()
    {
        score = 0;
        currentQuestionIndex = 0;
        endPanel.SetActive(false);
        quizPanel.SetActive(true);
        ShowQuestion();
    }

    void ShowQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            EndQuiz();
            return;
        }

        Question q = questions[currentQuestionIndex];
        questionText.text = q.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].gameObject.SetActive(i < q.answers.Length);
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.answers[i];
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }

        scoreText.text = $"Score: {score}/{questions.Length}";
    }

    void OnAnswerSelected(int index)
    {
        if (index == questions[currentQuestionIndex].correctAnswerIndex)
        {
            score++;
        }

        currentQuestionIndex++;
        ShowQuestion();
    }

    void EndQuiz()
    {
        quizPanel.SetActive(false);
        endPanel.SetActive(true);
        finalScoreText.text = $"Your total score {score} out of {questions.Length}!";
        StartCoroutine(GoToNextLevel());

    }

    IEnumerator GoToNextLevel()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<LevelManager>().NextLevel();
    }
}

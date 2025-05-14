using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using System;

[System.Serializable]
public class QuestionData
{
    public string questionText;
    public string[] answers;
    public int correctAnswerIndex;
}

public class QuizManagerController : MonoBehaviourPunCallbacks
{
    [Header("UI Elements")]
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI scoreText;
    public GameObject quizPanel;
    public GameObject endPanel;
    public TextMeshProUGUI finalScoreText;

    [Header("Quiz Data")]
    public QuestionData[] questions;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        // Everyone sees the panel
        quizPanel.SetActive(true);
        endPanel.SetActive(false);

        if (PhotonNetwork.LocalPlayer.IsQuizMaster())
        {
            score = 0;
            currentQuestionIndex = 0;
            ShowQuestion();
        }
    }

    void ShowQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            EndQuiz();
            return;
        }

        QuestionData q = questions[currentQuestionIndex];
        questionText.text = q.questionText;

        bool isQuizMaster = PhotonNetwork.LocalPlayer.IsQuizMaster();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].gameObject.SetActive(i < q.answers.Length);
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.answers[i];
            answerButtons[i].onClick.RemoveAllListeners();

            if (isQuizMaster)
            {
                answerButtons[i].interactable = true;
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
            }
            else
            {
                answerButtons[i].interactable = false;
            }
        }

        // Everyone sees the Quiz Master's score (optional)
        if (isQuizMaster)
            scoreText.text = $"Score: {score}/{questions.Length}";
        else
            scoreText.text = $"Quiz in progress...";
    }

    void OnAnswerSelected(int index)
    {
        if (!PhotonNetwork.LocalPlayer.IsQuizMaster())
            return;

     
        photonView.RPC("SubmitAnswerRPC", RpcTarget.All, index);
    }

    [PunRPC]
    void SubmitAnswerRPC(int index)
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

        if (PhotonNetwork.LocalPlayer.IsQuizMaster())
        {
            finalScoreText.text = $"Your total score: {score} out of {questions.Length}!";
        }
        else
        {
            finalScoreText.text = $"Quiz complete. Please check results with Quiz Master.";
        }

        StartCoroutine(GoToNextLevel());
    }

    IEnumerator GoToNextLevel()
    {
        yield return new WaitForSeconds(2f);

        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.NextLevel();
        }

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayMusic();
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

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
        quizPanel.SetActive(true);
        endPanel.SetActive(false);

        if (PhotonNetwork.IsMasterClient)
        {
            score = 0;
            currentQuestionIndex = 0;
            BroadcastQuestion(currentQuestionIndex); // Send question to everyone
        }
    }

    void BroadcastQuestion(int questionIndex)
    {
        if (questionIndex < questions.Length)
        {
            QuestionData q = questions[questionIndex];
            photonView.RPC("DisplayQuestionRPC", RpcTarget.All, questionIndex, q.questionText, q.answers[0], q.answers[1], q.answers[2], q.answers[3]);
        }
        else
        {
            photonView.RPC("EndQuizRPC", RpcTarget.All, score);
        }
    }



     public override void OnPlayerEnteredRoom(Player newPlayer)             // We send the request to all players to update the master current question.
    {
        if (PhotonNetwork.IsMasterClient)
        {
            QuestionData q = questions[currentQuestionIndex];
            photonView.RPC("DisplayQuestionRPC", newPlayer, currentQuestionIndex,
                q.questionText,
                q.answers[0], q.answers[1], q.answers[2], q.answers[3]);
        }
    }


    [PunRPC]
    void DisplayQuestionRPC(int questionIndex, string question, string ans1, string ans2, string ans3, string ans4)
    {
        currentQuestionIndex = questionIndex;

        questionText.text = question;

        string[] answers = new string[] { ans1, ans2, ans3, ans4 };
        bool isQuizMaster = PhotonNetwork.LocalPlayer.IsQuizMaster();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].gameObject.SetActive(i < answers.Length);
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[i];
            answerButtons[i].onClick.RemoveAllListeners();

            if (isQuizMaster)
            {
                int index = i;
                answerButtons[i].interactable = true;
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
            }
            else
            {
                answerButtons[i].interactable = false;
            }
        }

        scoreText.text = isQuizMaster ? $"Score: {score}/{questions.Length}" : "Quiz in progress...";
    }

    void OnAnswerSelected(int selectedIndex)
    {
        if (!PhotonNetwork.LocalPlayer.IsQuizMaster())
            return;

        if (selectedIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            score++;
        }
   
        currentQuestionIndex++;
        BroadcastQuestion(currentQuestionIndex); 
    }




    [PunRPC]
    void EndQuizRPC(int finalScore)
    {
        quizPanel.SetActive(false);
        endPanel.SetActive(true);

        if (PhotonNetwork.LocalPlayer.IsQuizMaster())
            finalScoreText.text = $"Your total score: {finalScore} out of {questions.Length}!";
        else
            finalScoreText.text = $"Quiz complete. Please check results with Quiz Master.";

        StartCoroutine(GoToNextLevel());
    }

    IEnumerator GoToNextLevel()
    {
        yield return new WaitForSeconds(2f);
        LevelManager manager = FindObjectOfType<LevelManager>();
        if (manager != null) manager.NextLevel();
        if (SoundManager.Instance != null) SoundManager.Instance.PlayMusic();
    }
}

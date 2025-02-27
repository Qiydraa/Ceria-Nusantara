using UnityEngine;
using UnityEngine.UI;
using TMPro; // Jika menggunakan TextMeshPro

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswerIndex;
    }

    public TextMeshProUGUI questionText; 
    public Button[] answerButtons; 
    public Question[] questions; 
    private int currentQuestionIndex = 0;

    void Start()
    {
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        Question currentQuestion = questions[currentQuestionIndex];
        questionText.text = currentQuestion.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];
            int index = i; 
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
    }

    void CheckAnswer(int index)
    {
        if (index == questions[currentQuestionIndex].correctAnswerIndex)
        {
            Debug.Log("Jawaban Benar!");
        }
        else
        {
            Debug.Log("Jawaban Salah!");
        }

        NextQuestion();
    }

    void NextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            Debug.Log("Kuis Selesai!");
        }
    }
}

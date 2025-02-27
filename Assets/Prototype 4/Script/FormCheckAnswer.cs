using UnityEngine;
using UnityEngine.UI;
using TMPro; // Jika menggunakan TextMeshPro

public class FormCheckAnswer : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TMP_InputField answerInputField;
    public TextMeshProUGUI feedbackText;
    public Button submitButton;
    
    private string correctAnswer = "Unity"; // Contoh jawaban benar

    void Start()
    {
        submitButton.onClick.AddListener(CheckAnswer);
    }

    void CheckAnswer()
    {
        string playerAnswer = answerInputField.text.Trim().ToLower(); // Normalisasi input
        string correct = correctAnswer.ToLower();

        if (playerAnswer == correct)
        {
            feedbackText.text = "Jawaban Benar!";
            feedbackText.color = Color.green;
        }
        else
        {
            feedbackText.text = "Jawaban Salah!";
            feedbackText.color = Color.red;
        }
    }
}

using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    private void Update()
    {
        // Получаем текущее время игры
        float currentTime = Time.time;

        // Форматируем время в формат "чч:мм:сс"
        string formattedTime = FormatTime(currentTime);

        // Обновляем текст в поле TextMeshPro
        textMesh.text = "current Time: " + formattedTime;
    }

    // Функция для форматирования времени в часы:минуты:секунды
    private string FormatTime(float time)
    {
        int hours = Mathf.FloorToInt(time / 3600);
        int minutes = Mathf.FloorToInt((time % 3600) / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }
}
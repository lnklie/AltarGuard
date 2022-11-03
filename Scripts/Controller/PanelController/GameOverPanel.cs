using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Image gameOverImage = null;
    [SerializeField] private Button retryButton = null;

    public void StartGameOver()
    {
        StartCoroutine(GameOver());
    }

    public IEnumerator GameOver()
    {
        this.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        gameOverImage.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
    }
}

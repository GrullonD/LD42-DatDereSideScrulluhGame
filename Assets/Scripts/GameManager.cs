using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    bool gameHasEnded = false;
    [SerializeField] float restartDelay = 3f;

    [SerializeField] GameObject player;
    public int score;

    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject gameOverUI;
    
    [SerializeField] GameObject gameOverScoreText;

    private void Start() {
        player = GameObject.FindWithTag("Player");
    }

    public void EndGame() {
        if(gameHasEnded == false) {
            gameHasEnded = true;

            // Blur background

            // Show game over menu
            mainMenuUI.SetActive(false);
            gameOverUI.SetActive(true);

            // Show score
            score = player.GetComponent<PlayerScore>().playerScore;
            gameOverScoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;

            // Restart Game
            Invoke("Restart", restartDelay);
        }

    }

    void Restart() {
        gameHasEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

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

    [SerializeField] GameObject heart;
    [SerializeField] GameObject heartSpawnZone;
    [SerializeField] int scoreToSpawnHeart = 200;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public bool gaveHeart = false;
    public int gaveHeartScore;

    private void Start() {
        player = GameObject.FindWithTag("Player");
        minX = heartSpawnZone.GetComponent<Renderer>().bounds.min.x;
        maxX = heartSpawnZone.GetComponent<Renderer>().bounds.max.x;
        minY = heartSpawnZone.GetComponent<Renderer>().bounds.min.y;
        maxY = heartSpawnZone.GetComponent<Renderer>().bounds.max.y;
    }

    private void Update() {
        CheckScore();
    }

    private void CheckScore() {
        score = player.GetComponent<PlayerScore>().playerScore;

        if(gaveHeart == false && (score % scoreToSpawnHeart == 0)) {
            gaveHeart = true;
            gaveHeartScore = score;
            GiveHealth();
        }

        if(gaveHeart == true && gaveHeartScore != score) {
            gaveHeart = false;
        }
    }

    public void GiveHealth() {
        Vector3 spawnLocation = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
        Instantiate(heart, spawnLocation, Quaternion.identity);
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

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    bool gameHasEnded = false;
    [SerializeField] float restartDelay = 3f;

	public void EndGame() {
        if(gameHasEnded == false) {
            gameHasEnded = true;

            // Blur background

            // Show Score
            // Restart Game
            Invoke("Restart", restartDelay);
        }

    }

    void Restart() {
        gameHasEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class finishLine : MonoBehaviour
{
    public GameObject ErrorDialogue;

    void OnTriggerEnter2D(Collider2D collision){

        if (collision.CompareTag("Player"))
        {
            string currentScene = SceneManager.GetActiveScene().name;
            string nextScene = "";


            if (currentScene == "Fase1" && GameManager.instance.collectedItems["book"] == 3)
            {
                nextScene = "Fase2";
            }
            else if (currentScene == "Fase2" && GameManager.instance.collectedItems["book"] == 6)
            {
                nextScene = "GameOver";
            }
            else{
                ErrorDialogue.SetActive(true);
            }
            
            SceneManager.LoadScene(nextScene);
            
        }
    }


}

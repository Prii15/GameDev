using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class StartControl : MonoBehaviour
{ public GameObject loadingScreen;   // Referência ao painel de carregamento
    public Slider progressBar;         // Referência à barra de progresso
    public GameObject controlsPanel;
    public GameObject canvasButtons;
    public GameObject victoryText;
    public GameObject loseText;

    public void LoadScene(string sceneName)
    {
        //Reseta as configurações
        GameManager.instance.collectedItems["book"] = 0;
        GameManager.score = 0;
        GameManager.playerLives = GameManager.maxLives;
        GameManager.win = true;

        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Ativar o painel de carregamento (apenas enquanto a cena está carregando)
        loadingScreen.SetActive(true);

        // Iniciar o carregamento da cena de forma assíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Impedir que a cena seja carregada automaticamente
        asyncLoad.allowSceneActivation = false;

        // Enquanto a cena está carregando, atualize a barra de progresso
        while (!asyncLoad.isDone)
        {
            // Atualizar a barra de progresso com o valor de carregamento
            progressBar.value = asyncLoad.progress;

            // Quando o carregamento atingir 90% (0.9), a cena já está praticamente pronta
            if (asyncLoad.progress >= 0.9f)
            {
                progressBar.value = 1f; // Completa a barra de progresso
                // A cena será ativada após o carregamento estar completo
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        // Após a cena ser carregada, desativa a tela de carregamento
        loadingScreen.SetActive(false);
    }

    public void OpenBookThenShowSettings()
    {
        StartCoroutine(WaitBookOpenThenShowSettings());
    }

    private IEnumerator WaitBookOpenThenShowSettings()
    {
        canvasButtons.SetActive(false);
        
        menuBook bookScript = FindObjectOfType<menuBook>();
        
        if (bookScript != null)
        {
            bookScript.ToggleBook(); // Inicia a animação de abrir o livro
            
            // Espera até que a animação realmente tenha começado
            yield return new WaitUntil(() => bookScript.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);
            
            // Agora espera o livro abrir completamente
            yield return new WaitUntil(() => bookScript.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        }

        // Agora mostra o painel de controles/configurações
        controlsPanel.SetActive(true);
    }

    public void CloseSettingsAfterBookCloses()
    {
        StartCoroutine(WaitBookCloseThenShowMainMenu());
    }

    private IEnumerator WaitBookCloseThenShowMainMenu()
    {
        // Esconde o painel de configurações imediatamente
        controlsPanel.SetActive(false);

        // Pede pro livro fechar
        menuBook bookScript = FindObjectOfType<menuBook>();
        if (bookScript != null)
        {
            bookScript.ToggleBook(); // Fecha o livro
            // Espera a animação de fechar terminar
            yield return new WaitForSeconds(bookScript.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }

        // Só depois, volta a mostrar os botões do menu principal
        canvasButtons.SetActive(true);
    }

    public void finalText(){
        if (GameManager.win)
        {
            victoryText.SetActive(true);
            loseText.SetActive(false);
        }
        else
        {
            victoryText.SetActive(false);
            loseText.SetActive(true);
        }
    }

    public void Quit()
    {
        // Se o jogo estiver rodando no editor
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Se for o jogo compilado, fecha o aplicativo
            Application.Quit();
        #endif
    }

    void Start(){
        if(SceneManager.GetActiveScene().name == "GameOver"){
            finalText();
        }
    }

}

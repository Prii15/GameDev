using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CollectableUI : MonoBehaviour
{
    public static CollectableUI instance;

    public GameObject collectablePrefab; // Prefab do coletável (deve ter um Image e um Text)
    public Transform collectablesParent; // Objeto vazio no Canvas para armazenar os coletáveis
    private Dictionary<string, (GameObject obj, TextMeshProUGUI text)> collectablesUI = new Dictionary<string, (GameObject, TextMeshProUGUI)>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Evita duplicatas
        }
    }

    public void InitializeCollectable(string collectableName, Sprite collectableSprite, int initialAmount)
    {
        if (!collectablesUI.ContainsKey(collectableName))
        {
            // Criar um novo item na UI mesmo com valor 0
            GameObject collectableObj = Instantiate(collectablePrefab, collectablesParent);
            Image collectableImage = collectableObj.transform.Find("Image").GetComponent<Image>();
            TextMeshProUGUI collectableText = collectableObj.transform.Find("Text").GetComponent<TextMeshProUGUI>();

            collectableImage.sprite = collectableSprite;
            collectablesUI[collectableName] = (collectableObj, collectableText);
        }

        // Inicializa o valor como 0 ou o valor inicial definido
        UpdateCollectablesUI(collectableName, initialAmount);
    }

    public void UpdateCollectablesUI(string collectableName, int amount)
    {
        if (collectablesUI.ContainsKey(collectableName))
        {
            collectablesUI[collectableName].text.text = amount + "x";
        }
    }

}

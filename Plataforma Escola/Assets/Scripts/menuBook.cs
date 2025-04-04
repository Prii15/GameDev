using UnityEngine;
using System.Collections;

public class menuBook : MonoBehaviour
{
    public Animator animator;
    private bool isOpen = false;

    void Start(){
        animator = GetComponent<Animator>();        // Inicializa o animator
    }

    private void AnimationController(){
        animator.SetBool("isOpen", isOpen);
    }

    void Update(){
        AnimationController();
    }

    public void ToggleBook()
    {
        if (!isOpen)
        {
            // isOpen = true;
            StartCoroutine(OpenBook());
        }
        else
        {
            // isOpen = false;
            StartCoroutine(CloseBook());
        }
    }

    private IEnumerator OpenBook()
    {
        animator.Play("bookOpen", 0, 0f); // Reinicia a animação do início
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Espera a animação acabar
        isOpen = true;
    }

    private IEnumerator CloseBook()
    {
        animator.Play("bookOpen 1", 0, 0f); // Começa do fim
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Espera acabar
        isOpen = false;
    }
}

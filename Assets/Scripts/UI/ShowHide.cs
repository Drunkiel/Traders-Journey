using UnityEngine;

public class ShowHide : MonoBehaviour
{
    public bool isShown;
    [SerializeField] private Animator anim;

    [SerializeField] private string showAnimationName;
    [SerializeField] private string hideAnimationName;

    public void ShowHideAnimation()
    {
        anim.SetBool("isShown", !anim.GetBool("isShown"));
        isShown = anim.GetBool("isShown");

        if (isShown) anim.Play(hideAnimationName);
        else anim.Play(showAnimationName);
    }
}

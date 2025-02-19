using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour {
    [SerializeField] private Animator anim;
    protected int eaten;
    private string currentAnimName;
    protected bool isWinning;

    protected void ChangeAnim(string animName) {
        if(currentAnimName != animName) {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
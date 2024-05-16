using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    [SerializeField] public int key=-1;
    Animator anim;
    bool open;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Open() {
        anim.SetTrigger("Open");
        open=true;
    }
    public void Open(Inventory inv, Animator canvasAnimator)
    {
        if (key > -1 && inv.IsKeyUnlocked(key) || key <= -1)
        {
            anim.SetTrigger("Open");
            open = true;
        }
        else
        {
            switch (key) {
                case 0:
                    canvasAnimator.Play("BlueFlash");
                    break;
                case 1:
                    canvasAnimator.Play("YellowFlash");
                    break;
                case 2:
                    canvasAnimator.Play("RedFlash");
                    break;
            }
        }
    }

    public bool GetOpen() {
        return open;
    }
}

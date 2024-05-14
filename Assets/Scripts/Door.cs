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
    public void Open(Inventory inv)
    {
        if (key > -1 && inv.IsKeyUnlocked(key) || key <= -1)
        {
            anim.SetTrigger("Open");
            open = true;
        }
        else 
            Debug.Log("Needs Key: " + key);
    }

    public bool GetOpen() {
        return open;
    }
}

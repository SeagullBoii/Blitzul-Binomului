using UnityEngine;

public class Inventory : MonoBehaviour
{
    bool[] unlockedKeys = new bool[3]; //Blue, Yellow, Red

    public bool IsKeyUnlocked(int index)
    {
        return unlockedKeys[index];
    }

    public void UnlockKey(int index)
    {
        unlockedKeys[index] = true;
    }
}

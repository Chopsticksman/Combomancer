using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    protected int dmg;
    public abstract void Play(Vector3 location);
}

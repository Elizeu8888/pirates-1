using UnityEngine.Events;
using UnityEngine;

public class EventPass : MonoBehaviour
{
    public UnityEvent use;
    public UnityEvent altUse;

    public AudioSource player;
    public AudioClip stepL;
    public AudioClip stepR;

    public Weapon weaponscript;

    //public SoundManager soundscript;

    public void Use()
    {
        use.Invoke();
        print("gggg");
    }

    public void AltUse()
    {
        altUse.Invoke();
    }

    public void Damage()
    {
        weaponscript.Damage();
    }


    public void StepL()
    {
        player.PlayOneShot(stepL, 0.1f);
    }
    public void StepR()
    {
        player.PlayOneShot(stepR, 0.1f);
    }
    public void Land()
    {
        player.PlayOneShot(stepL, 0.6f);
    }

}

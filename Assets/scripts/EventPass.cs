using UnityEngine.Events;
using UnityEngine;

public class EventPass : MonoBehaviour
{
    public UnityEvent use;
    public UnityEvent altUse;
    public void Use()
    {
        use.Invoke();
        print("gggg");
    }

    public void AltUse()
    {
        altUse.Invoke();
    }



}

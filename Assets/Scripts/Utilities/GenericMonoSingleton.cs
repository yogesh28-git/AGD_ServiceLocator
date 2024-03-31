using UnityEngine;

public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
{
    private T instance;
    public T Instance {  get { return instance; } }

    private void Awake( )
    {
        if (instance == null )
        {
            instance = this as T;
        }
        else
        {
            Destroy( this.gameObject );
        }
    }
}

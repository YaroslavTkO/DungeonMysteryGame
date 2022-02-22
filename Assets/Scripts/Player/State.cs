using UnityEngine;

[System.Serializable]
public abstract class State
{
    public PlayerController Controller;
    protected Vector2 _direction;
    public abstract State Update();
    public abstract Vector2 HandleInput();
    
    public abstract State ChangeState(State state);

    public abstract Vector2 FixedUpdate();

}

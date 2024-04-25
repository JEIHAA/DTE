using UnityEngine;

public interface ICharacterState
{
    void EnterState();
    void UpdateState();
    void ExitState();
}

public class CharacterStateManager: MonoBehaviour
{
    protected ICharacterState curState;

    public void ChangeState(ICharacterState _nextState)
    {

        if (_nextState == curState)
        {
            return;
        }

        if (curState != null)
        {
            curState.ExitState();
        }

        curState = _nextState;
        curState.EnterState();
    }

    public void Update()
    {
        if (curState != null)
            curState.UpdateState();
    }
}

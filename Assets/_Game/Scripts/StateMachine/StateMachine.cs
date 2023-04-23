using ImpossibleOdds.Runnables;

public class StateMachine<T> : IRunnable, IFixedRunnable
{
	public State<T> CurrentState { get; private set; }
	public State<T> PreviousState { get; private set; }

	bool _inTransition = false;

#region public
	public StateMachine()
    {
		SceneRunner.Get.AddUpdate(this);
		SceneRunner.Get.AddFixedUpdate(this);
    }

	// pass down Update ticks to States, since they won't have a MonoBehaviour
	public void Tick()
	{
		// simulate update ticks in states
		if (CurrentState != null && !_inTransition)
			CurrentState.Tick();
	}

	public void FixedTick()
	{
		// simulate fixedUpdate ticks in states
		if (CurrentState != null && !_inTransition)
			CurrentState.FixedTick();
	}

	public void ChangeState(State<T> newState)
	{
		// ensure we're ready for a new state
		if (CurrentState == newState || _inTransition || newState == null)
			return;

		ChangeStateRoutine(newState);
	}

	public void RevertState()
	{
		if (PreviousState != null)
			ChangeState(PreviousState);
	}
	#endregion

#region private
	void ChangeStateRoutine(State<T> newState)
	{
		_inTransition = true;
		// begin our exit sequence, to prepare for new state
		if (CurrentState != null)
			CurrentState.Exit();
        // save our current state, in case we want to return to it
        //if (PreviousState != null)
        //	PreviousState = CurrentState;
        PreviousState = CurrentState;


		CurrentState = newState;

		// begin our new Enter sequence
		if (CurrentState != null)
			CurrentState.Enter();

		_inTransition = false;
	}
	#endregion

	public void Update()
	{
		Tick();
	}

	public void FixedUpdate()
	{
		FixedTick();
	}
}

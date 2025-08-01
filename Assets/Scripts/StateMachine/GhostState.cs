namespace Horror.StateMachine
{
    internal abstract class GhostState<S> : IState<GhostPayload>
        {
            protected S settings;
            public GhostState(S settings)
            {
                this.settings = settings;
            }
            public virtual void EnterState(GhostPayload payload) { }
            public virtual void ExitState(GhostPayload payload) { }
            public virtual IState<GhostPayload> Update(GhostPayload payload) { return this; }
        }
}

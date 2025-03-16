using UnityEngine;

namespace StateMachine
{
    public abstract class State
    {
        public virtual void Enter()
        {

        }

        public virtual void TransitionCheck()
        {

        }

        public virtual void HandleInput()
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
}

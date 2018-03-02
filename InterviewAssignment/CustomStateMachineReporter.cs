using Appccelerate.StateMachine.Machine;
using System;
using System.Collections.Generic;

namespace InterviewAssignment
{

    /// <summary>
    /// Provides way to customize state reporting.
    /// 
    /// By default you cannot get container of states from Appccelerate state machine.
    /// This class can be used for that. Also it has StateToString method to enable
    /// printing hierarchical states.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TEvent"></typeparam>
    public class CustomStateMachineReporter<TState, TEvent> : IStateMachineReport<TState, TEvent>
            where TState : IComparable
    where TEvent : IComparable

    {
        IEnumerable<IState<TState, TEvent>> myStates;

        public IEnumerable<IState<TState, TEvent>> States
        {
            get
            {
                return myStates;
            }
        }

        public void Report(string name, IEnumerable<IState<TState, TEvent>> states, Initializable<TState> initialStateId)
        {
            myStates = states;
        }

        public string StateToString(TState state, string separator = ".")
        {
            string stringState = state.ToString(); // Converts state to string
            switch (stringState) //switch case
            {
                case "Initializing": //Substates of "Down"
                case "Error":
                case "NotInitialized":
                    return "Down" + separator + stringState;

                case "PoweringOff":     //Substates of "Idle"
                case "PoweringOn":      //"Idle" is substate of "Ready"
                case "PowerOn":
                case "PowerOff":
                    return "Ready" + separator + "Idle" + separator + stringState;

                case "AxesIdle":    //Substates of "Processing
                case "AxesMoving":  //"Processing" is substate of "Ready"
                    return "Ready" + separator + "Processing" + separator + stringState;

                default:
                    return "Down.Error";    //Returns Error state if undefined state is met
            }
        }
    }
}


﻿using Appccelerate.StateMachine.Machine;
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
            string result = "";
            List<string> ssList = new List<string>();

            var enumerator = myStates.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (enumerator.Current.ToString() == state.ToString())
                {
                    var cState = enumerator.Current;

                    while (cState.SuperState != null)
                    {
                        cState = cState.SuperState;
                        ssList.Insert(0, cState.ToString());
                    }

                    foreach (var ss in ssList)
                    {
                        result += ss + separator;
                    }
                }
            }

            return result + state.ToString();
        }
    }
}

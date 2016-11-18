using System;
using System.Collections.Generic;
using System.Linq;
using Patterns.Other._03_ActiveStateMachine.Common;

namespace UIApplication
{
    class TelephoneViewStateConfiguration : IViewStateConfiguration
    {
        public string DefaultViewState
        {
            get
            {
                foreach (var item in ViewStates.Values.Cast<TelephoneViewState>()
                    .Where(item => item.IsDefaultViewState))
                {
                    return item.Name;
                }
                throw new Exception("Missing default view state");
            }
            set
            {
                
            }
        }

        public string[] ViewStateList
        {
            get; set;
        }

        public Dictionary<string, object> ViewStates
        {
            get; set;
        }

        public TelephoneViewStateConfiguration()
        {
            InitViewStates();
        }

        private void InitViewStates()
        {
            ViewStates = new Dictionary<string, object>
            {
                {"ViewPhoneIdle", new TelephoneViewState("ViewPhoneIdle", false, false, true, true) },//Default view state
                {"ViewPhoneRings", new TelephoneViewState("ViewPhoneRings", true, false, true) },
                {"ViewTalking", new TelephoneViewState("ViewTalking", false, true, false) },
            };

            ViewStateList = new[] {"ViewPhoneIdle", "ViewPhoneRings", "ViewTalking"};
        }
    }
}

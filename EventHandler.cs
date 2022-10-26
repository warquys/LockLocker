using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MapGeneration.Distributors;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LockLocker
{
    public class EventHandler
    {
        #region Constructor & Destructor
        internal EventHandler()
        {
            AttachEvent();
        }
        #endregion

        #region Methods
        public void AttachEvent()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;

        }

        public void DetachEvent()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
        }
        #endregion

        #region Events
        private void OnRoundStarted()
        {
            Plugin.Instance.LockerChambers.Clear();

            // Map.Lockers use less bc don't list all of the LOCKER !
            var allLocker = UnityEngine.Object.FindObjectsOfType<Locker>(); // Note: i lose 4h to try to find why my code not work!

            foreach (var locker in allLocker)
            {
                foreach (var chamber in locker.Chambers)
                    Plugin.Instance.LockerChambers.Add(chamber);
            }
        }
        #endregion
    }
}

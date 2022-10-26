using Exiled.API.Enums;
using Exiled.API.Features;
using HarmonyLib;
using MapGeneration.Distributors;
using System;
using System.Collections.Generic;

namespace LockLocker
{
    public class Plugin : Plugin<Config>
    {
        public override string Prefix => "LockLocker";
        public override string Name => "Lock Locker";
        public override string Author => "VT";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override PluginPriority Priority => PluginPriority.Higher;

        public static Plugin Instance { get; private set; }

        public List<LockerChamber> LockerChambers { get; internal set; } = new();
        public Harmony Harmony { get; private set; }
        public bool LockerLock { get; set; }
        public EventHandler EventHandler { get; private set; }


        public override void OnEnabled()
        {
            base.OnEnabled();
            if (Instance is null)
                Instance = this;
            Patch();
            if (EventHandler == null)
                EventHandler = new EventHandler();
            else
                EventHandler.AttachEvent();
        }

        public override void OnDisabled()
        {
            Unpatch();
            EventHandler.DetachEvent();
            base.OnDisabled();
        }

        public void Patch()
        {
            if (Harmony == null)
                Harmony = new Harmony(Name);
            Harmony.PatchAll();
        }

        public void Unpatch()
        {
            Harmony.UnpatchAll();
        }
    }
}
// Copyright © 2008 David S. Bakin
// This work licensed under The Code Project Open License (CPOL) 1.02

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

namespace FergusonMoriyam.Workflow.Application.Runtime
{
    /// <summary>
    /// A class which lets you hook any or all events raised by an particular
    /// object.  Handles C# events which don't match the .NET Framework design
    /// pattern for events.
    /// </summary>
    /// <remarks>
    /// To use, create a TracerEx instance supplying a target object and a delegate
    /// which will receive the hooked events (the delegate is of type OnEventHandler,
    /// <see cref="OnEventHandler"/>).  Then hook the events you want to trace,
    /// by name.  (Get the names via the property <see cref="EventNames"/>.)
    /// </remarks>
    public sealed class TracerEx : IDisposable
    {
        /// <summary>
        /// Specifies the event handler that the user creates to hook events
        /// from the target object.
        /// </summary>
        /// <param name="target">Object that this tracer is hooking</param>
        /// <param name="eventName">Name of the event the target raised</param>
        /// <param name="parameters">An array of objects that are the
        /// parameters passed to the event</param>
        public delegate void OnEventHandler(object target,
                                             string eventName,
                                             object[] parameters);

        /// <summary>
        /// Constructs an event TracerEx.
        /// </summary>
        /// <param name="target">The object whose events are to be hooked</param>
        /// <param name="handler">User-supplied method gets called whenever the 
        /// target raises a hooked event</param>
        public TracerEx(object target,
                         OnEventHandler handler)
        {
            Debug.Assert(target != null);
            m_type = (Type)target;
            m_target = target;
            m_handler = handler;
        }

        /// <summary>
        /// Unhooks all events when TracerEx is Disposed.
        /// </summary>
        public void Dispose()
        {
            if (EventsHookedCount > 0)
            {
                UnhookAllEvents();
            }
            m_target = null;
            m_handler = null;
        }

        #region Target of this TracerEx
        private OnEventHandler m_handler;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type m_type;
        /// <summary>
        /// The target's type
        /// </summary>
        public Type TheType
        {
            [DebuggerStepThrough]
            get
            {
                return m_type;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private object m_target;
        /// <summary>
        /// The target object of this TracerEx
        /// </summary>
        public object TheTarget
        {
            [DebuggerStepThrough]
            get
            {
                return m_target;
            }
        }
        #endregion

        #region Information on the target classes' events
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private EventInfo[] events = null;
        /// <summary>
        /// Information on all public events that can be raised by the target
        /// </summary>
        private EventInfo[] Events
        {
            get
            {
                if (events == null)
                {
                    events = m_type.GetEvents(BindingFlags.Instance |
                                               BindingFlags.Static |
                                               BindingFlags.Public |
                                               BindingFlags.FlattenHierarchy);

                    // Now process events to strip out the ones which we won't
                    // hook.  Those are the events with more than 10
                    // parameters, or which have any value type, ref, or out
                    // parameters.

                    List<EventInfo> okEvents = new List<EventInfo>();
                    List<string> okEventNames = new List<string>();
                    Dictionary<EventInfo, int> okEventParamCount = new Dictionary<EventInfo, int>();
                    List<string> notOkEventNames = new List<string>();

                    foreach (EventInfo eventInfo in Events)
                    {
                        if (IsTraceableEvent(eventInfo))
                        {
                            okEvents.Add(eventInfo);
                            okEventNames.Add(eventInfo.Name);
                            okEventParamCount[eventInfo] = EventParameterCount(eventInfo);
                        }
                        else
                        {
                            notOkEventNames.Add(eventInfo.Name);
                        }
                    }

                    okEventNames.Sort();
                    notOkEventNames.Sort();

                    events = okEvents.ToArray();
                    eventNames = okEventNames.ToArray();
                    eventParamCount = okEventParamCount;
                    untraceableEventNames = notOkEventNames.ToArray();
                }

                return events;
            }
        }

        /// <summary>
        /// A map from EventInfo to the number of parameters the event takes.
        /// </summary>
        private Dictionary<EventInfo, int> eventParamCount = null;

        /// <summary>
        /// A Set - if the key is present then that EventInfo is a static
        /// event
        /// </summary>
        private Dictionary<EventInfo, object> isStaticEvent = null;

        /// <summary>
        /// Predicate returns true iff the given event is a static event of 
        /// the target's class
        /// </summary>
        private bool IsStaticEvent(EventInfo eventInfo)
        {
            if (isStaticEvent == null)
            {
                // This is odd:  Using Type.FindMembers with
                // MemberTypes.Event and BindingFlags.Static returns ALL
                // events, instance and static, where Type.GetEvents with
                // BindingFlags.Static will only return static events.
                EventInfo[] staticEvents =
                    m_type.GetEvents(BindingFlags.Static |
                                      BindingFlags.Public |
                                      BindingFlags.FlattenHierarchy);
                isStaticEvent = new Dictionary<EventInfo, object>();
                foreach (EventInfo eventInfo2 in staticEvents)
                {
                    isStaticEvent[eventInfo2] = null;
                }
            }
            return isStaticEvent.ContainsKey(eventInfo);
        }

        /// <summary>
        /// Predicate returns true iff the given string is the name of a 
        /// static event of the target's class
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public bool IsStaticEvent(string eventName)
        {
            if (!IsValidEvent(eventName))
            {
                throw new ArgumentException("No event with that name");
            }
            return IsStaticEvent(Array.Find(Events, EventHasName(eventName)));
        }

        /// <summary>
        /// Determine if an event is traceable.  To be traceable there must
        /// must not be any value type, ref, or out parameters, and must not
        /// have more than 10 parameters, and the return type must be void.
        /// </summary>
        private bool IsTraceableEvent(EventInfo eventInfo)
        {
            // First, from the EventInfo get the delegate type.
            Type eventHandlerType = eventInfo.EventHandlerType;
            Debug.Assert(eventHandlerType.BaseType == typeof(System.MulticastDelegate));
            if (eventHandlerType.BaseType != typeof(System.MulticastDelegate))
            {
                // Must not have been an event after all?!?
                return false;
            }

            // The signature we want to look at is the delegate's Invoke method.
            MethodInfo invoke = eventHandlerType.GetMethod("Invoke");
            Debug.Assert(invoke != null);
            if (invoke == null)
            {
                // Must not have been a delegate after all?!?
                return false;
            }

            // Get the delegate's parameter list.
            ParameterInfo[] parameters = invoke.GetParameters();

            if (parameters.Length > 10)
            {
                return false;
            }

            // Make sure there aren't any value type, ref, or out parameters
            foreach (ParameterInfo p in parameters)
            {
                if (p.IsOut ||
                     p.IsRetval ||
                     p.ParameterType.IsValueType ||
                     p.ParameterType.IsByRef)
                {
                    return false;
                }
            }

            // Return type must be void
            if (invoke.ReturnType != typeof(void))
            {
                return false;
            }

            // All tests pass - this is a standard .NET Framework event type
            return true;
        }

        /// <summary>
        /// Return the number of useful parameters in the signature of the event's
        /// delegate, but not more than 10.
        /// </summary>
        private int EventParameterCount(EventInfo eventInfo)
        {
            // First, from the EventInfo get the delegate type.
            Type eventHandlerType = eventInfo.EventHandlerType;
            Debug.Assert(eventHandlerType.BaseType == typeof(System.MulticastDelegate));
            if (eventHandlerType.BaseType != typeof(System.MulticastDelegate))
            {
                // Must not have been an event after all?!?
                return 0;
            }

            // The signature we want to look at is the delegate's Invoke method.
            MethodInfo invoke = eventHandlerType.GetMethod("Invoke");
            Debug.Assert(invoke != null);
            if (invoke == null)
            {
                // Must not have been a delegate after all?!?
                return 0;
            }

            // Get the delegate's parameter list...
            ParameterInfo[] parameters = invoke.GetParameters();

            if (parameters.Length > 10)
            {
                Debug.Assert(parameters.Length <= 10);
                return 0;
            }
            return parameters.Length;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] eventNames = null;
        private string[] EventNamesInternal
        {
            get
            {
                if (eventNames == null)
                {
                    EventInfo[] events = Events;
                    Debug.Assert(eventNames != null);
                }
                return eventNames;
            }
        }

        /// <summary>
        /// A list of the names of all the events that can be raised by the target
        /// </summary>
        public ReadOnlyCollection<string> EventNames
        {
            get
            {
                return new ReadOnlyCollection<string>(EventNamesInternal);
            }
        }

        /// <summary>
        /// Predicate returns true iff the given string is the name of an event
        /// the target can raise
        /// </summary>
        public bool IsValidEvent(string eventName)
        {
            return Array.BinarySearch(EventNamesInternal, eventName) >= 0;
            // (Relies on the EventNames being sorted.)
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] untraceableEventNames = null;
        /// <summary>
        /// A list of the names of all the events that can be raised by the
        /// target but that the TracerEx can't trace.  These events are those
        /// which don't follow the .NET Framework design pattern for events.
        /// </summary>
        public ReadOnlyCollection<string> UntraceableEventNames
        {
            get
            {
                if (untraceableEventNames == null)
                {
                    EventInfo[] events = Events;
                    Debug.Assert(untraceableEventNames != null);
                }
                return new ReadOnlyCollection<string>(untraceableEventNames);
            }
        }
        #endregion

        #region Keeping track of hooked events
        /// <summary>
        /// Dictionary maps event names to the delegate subscribing to the event
        /// </summary>
        private Dictionary<string, Delegate> m_hooks =
            new Dictionary<string, Delegate>();

        /// <summary>
        /// Returns number of events currently hooked
        /// </summary>
        public int EventsHookedCount
        {
            [DebuggerStepThrough]
            get
            {
                return m_hooks.Count;
            }
        }

        /// <summary>
        /// Predicate returns true iff event (specified by its name) is 
        /// currently hooked
        /// </summary>
        public bool IsHookedEvent(string eventName)
        {
            return m_hooks.ContainsKey(eventName);
        }
        #endregion

        #region Class EventProxy for thunking events with their string name
        /// <summary>
        /// An instance of this class is created for every event that is hooked. 
        /// It's purpose is to hold the event's name so that the user can use 
        /// only one method to handle all of the events being hooked on a given
        /// target object.
        /// </summary>
        private sealed class EventProxy
        {
            private readonly OnEventHandler m_handler;
            private readonly object m_target;
            private readonly string m_name;

            public EventProxy(TracerEx parent, object target, string name)
            {
                m_handler = parent.m_handler;
                m_target = target;
                m_name = name;
            }

            #region Event handlers
            public void OnEvent0()
            {
                if (m_handler != null)
                {
                    object[] parameters = new object[0];
                    m_handler.Invoke(m_target, m_name, parameters);
                }
            }

            public void OnEvent1(object p1)
            {
                if (m_handler != null)
                {
                    object[] parameters = new object[1];
                    parameters[0] = p1;
                    m_handler.Invoke(m_target, m_name, parameters);
                }
            }

            public void OnEvent2(object p1, object p2)
            {
                if (m_handler != null)
                {
                    object[] parameters = new object[2];
                    parameters[0] = p1;
                    parameters[1] = p2;
                    m_handler.Invoke(m_target, m_name, parameters);
                }
            }

            public void OnEvent3(object p1, object p2, object p3)
            {
                if (m_handler != null)
                {
                    object[] parameters = new object[3];
                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    m_handler.Invoke(m_target, m_name, parameters);
                }
            }

            public void OnEvent4(object p1, object p2, object p3, object p4)
            {
                if (m_handler != null)
                {
                    object[] parameters = new object[4];
                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;
                    m_handler.Invoke(m_target, m_name, parameters);
                }
            }

            public void OnEvent5(object p1, object p2, object p3, object p4, object p5)
            {
                if (m_handler != null)
                {
                    object[] parameters = new object[5];
                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;
                    parameters[4] = p5;
                    m_handler.Invoke(m_target, m_name, parameters);
                }
            }

            public void OnEvent6(object p1, object p2, object p3, object p4, object p5, object p6)
            {
                if (m_handler != null)
                {
                    object[] parameters = new object[6];
                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;
                    parameters[4] = p5;
                    parameters[5] = p6;
                    m_handler.Invoke(m_target, m_name, parameters);
                }
            }

            public void OnEvent7(object p1, object p2, object p3, object p4, object p5, object p6, object p7)
            {
                if (m_handler != null)
                {
                    object[] parameters = new object[7];
                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;
                    parameters[4] = p5;
                    parameters[5] = p6;
                    parameters[6] = p7;
                    m_handler.Invoke(m_target, m_name, parameters);
                }
            }

            public void OnEvent8(object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8)
            {
                if (m_handler != null)
                {
                    object[] parameters = new object[8];
                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;
                    parameters[4] = p5;
                    parameters[5] = p6;
                    parameters[6] = p7;
                    parameters[7] = p8;
                    m_handler.Invoke(m_target, m_name, parameters);
                }
            }

            public void OnEvent9(object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9)
            {
                if (m_handler != null)
                {
                    object[] parameters = new object[9];
                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;
                    parameters[4] = p5;
                    parameters[5] = p6;
                    parameters[6] = p7;
                    parameters[7] = p8;
                    parameters[8] = p9;
                    m_handler.Invoke(m_target, m_name, parameters);
                }
            }

            public void OnEvent10(object p1, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10)
            {
                if (m_handler != null)
                {
                    object[] parameters = new object[10];
                    parameters[0] = p1;
                    parameters[1] = p2;
                    parameters[2] = p3;
                    parameters[3] = p4;
                    parameters[4] = p5;
                    parameters[5] = p6;
                    parameters[6] = p7;
                    parameters[7] = p8;
                    parameters[8] = p9;
                    parameters[9] = p10;
                    m_handler.Invoke(m_target, m_name, parameters);
                }
            }
            #endregion

            /// <summary>
            /// The MethodInfos of the events raised in this class.
            /// </summary>
            public static MethodInfo[] OnEventMethodInfos = null;

            static EventProxy()
            {
                OnEventMethodInfos = new MethodInfo[11];
                for (int i = 0; i <= 10; i++)
                {
                    OnEventMethodInfos[i] =
                        typeof(EventProxy).GetMethod("OnEvent" + i.ToString());
                }
            }
        }
        #endregion

        #region Hooking and Unhooking Events
        /// <summary>
        /// Given an EventInfo for an event of the target instance's class, hook it
        /// </summary>
        private void HookEvent(EventInfo eventInfo)
        {
            // Each event is only hooked once
            if (TheTarget != null && !IsHookedEvent(eventInfo.Name))
            {
                // To hook an event: Create an EventProxy for it, then create
                // a delegate to the EventProxy's OnEventXXX method, then add the
                // delegate to the event's list.

                int parameterCount = eventParamCount[eventInfo];
                Debug.Assert(0 <= parameterCount && parameterCount <= 10);

                EventProxy proxy = new EventProxy(this, TheTarget, eventInfo.Name);
                MethodInfo hookMethod = EventProxy.OnEventMethodInfos[parameterCount];
                Delegate d = Delegate.CreateDelegate(eventInfo.EventHandlerType,
                                                      proxy,
                                                      hookMethod);

                m_hooks[eventInfo.Name] = d;
                eventInfo.AddEventHandler(TheTarget, d);
            }
        }

        /// <summary>
        /// Hook an event of the target instance's class, given its name
        /// </summary>
        public void HookEvent(string eventName)
        {
            if (!IsValidEvent(eventName))
            {
                throw new ArgumentException("No event with that name");
            }
            HookEvent(Array.Find(Events, EventHasName(eventName)));
        }

        /// <summary>
        /// Hook all events that the instance can raise
        /// </summary>
        public void HookAllEvents()
        {
            foreach (EventInfo eventInfo in Events)
            {
                HookEvent(eventInfo);
            }
        }

        /// <summary>
        /// Given an EventInfo for an event of the target instance's class, unhook it
        /// </summary>
        private void UnhookEvent(EventInfo eventInfo)
        {
            if (TheTarget != null && IsHookedEvent(eventInfo.Name))
            {
                eventInfo.RemoveEventHandler(TheTarget, m_hooks[eventInfo.Name]);
                m_hooks.Remove(eventInfo.Name);
            }
        }

        /// <summary>
        /// Unhook an event of the target instance's class, given its name
        /// </summary>
        public void UnhookEvent(string eventName)
        {
            if (!IsValidEvent(eventName))
            {
                throw new ArgumentException("No event with that name");
            }
            UnhookEvent(Array.Find(Events, EventHasName(eventName)));
        }

        /// <summary>
        /// Unhook all events that have been hooked
        /// </summary>
        public void UnhookAllEvents()
        {
            // Make a copy of m_hooks.Keys because it is going to get
            // modified during the foreach - m_hooks.Keys does not
            // return a copy of the Keys of a Dictionary, it returns the
            // actual KeysCollection.
            foreach (string name in new List<string>(m_hooks.Keys))
            {
                UnhookEvent(name);
            }
        }

        /// <summary>
        /// Factory that returns a predicate that returns true if the EventInfo
        /// it is passed is for the event whose name is passed to the factory
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        private static Predicate<EventInfo> EventHasName(string eventName)
        {
            return delegate(EventInfo eventInfo)
            {
                return eventInfo.Name == eventName;
            };
        }
        #endregion
    }
}

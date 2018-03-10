﻿using System;
using Redbus.Events;
using Redbus.Interfaces;

namespace Redbus
{
    internal class Subscription<TEventBase> : ISubscription where TEventBase : EventBase
    {
        public SubscriptionToken SubscriptionToken { get; private set; }

        public Subscription(Action<TEventBase> action, SubscriptionToken token)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (token == null)
            {
                throw new ArgumentNullException("token");
            }
            _action = action;

            SubscriptionToken = token;
        }

        public void Publish(EventBase eventItem)
        {
            if (!(eventItem is TEventBase))
                throw new ArgumentException("Event Item is not the correct type.");

            _action.Invoke(eventItem as TEventBase);
        }

        private readonly Action<TEventBase> _action;
    }
}

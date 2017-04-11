using System;
using System.Collections.Generic;
using Assets.Shared;
using Assets.Systems.Messaging;


namespace Assets.Source.Systems.Messaging {

    public class MessageDispatcher : BaseComponent, IMessageDispatcher {

        private Dictionary<Type, List<Delegate>> Handlers = new Dictionary<Type, List<Delegate>>();

        public void AddHandler<T>(Handler<T> handler) where T : Message {
            if (!Handlers.ContainsKey(typeof(T))) {
                Handlers[typeof(T)] = new List<Delegate>();
            }
            Handlers[typeof(T)].Add(handler);
        }

        public void Dispatch<T>(T message) where T : Message {
            if (!Handlers.ContainsKey(message.GetType())) {
                return;
            }

            foreach (var messageHandler in Handlers[message.GetType()]) {
                ((Handler<T>) messageHandler)(message);
            }
        }

        public void RemoveHandler<T>(Handler<T> handler) where T : Message {
            if (Handlers.ContainsKey(typeof(T))) {
                Handlers[typeof(T)].Remove(handler);
            }
        }

    }

}
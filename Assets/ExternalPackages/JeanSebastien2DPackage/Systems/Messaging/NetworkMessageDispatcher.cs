using System;
using System.Collections.Generic;
using Assets.Shared;
using UnityEngine;
using UnityEngine.Networking;


namespace Assets.Systems.Messaging {

    public class NetworkMessageDispatcher : BaseComponent, INetMessageDispatcher {

        private Dictionary<NetworkClient, List<Type>> Clients = new Dictionary<NetworkClient, List<Type>>();
        private Dictionary<Type, short> MsgIds = new Dictionary<Type, short>();
        private short msgMax = MsgType.Highest;

        
       public void AddHandler<T>(NetworkMessageDelegate handler) where T : NetMessage {
           if (Clients.Count < NetworkClient.allClients.Count) {
                AddClients();
            }
            foreach (var client in NetworkClient.allClients) {
                if (!Clients[client].Contains(typeof(T))) {
                    Clients[client].Add(typeof(T));
                }
            }
            if (!MsgIds.ContainsKey(typeof(T))) {
                //Debug.Log("New short" + typeof(T).ToString());

                msgMax += 1;
                MsgIds[typeof(T)] = msgMax;
            }

            foreach (var networkClient in NetworkClient.allClients) {
                if (!networkClient.handlers.ContainsKey(MsgIds[typeof(T)])) {
                    networkClient.RegisterHandler(MsgIds[typeof(T)], handler);
                }
                else if (networkClient.handlers[MsgIds[typeof(T)]] != handler && networkClient.handlers.ContainsKey(MsgIds[typeof(T)])) {
                    networkClient.RegisterHandler(MsgIds[typeof(T)], handler);
                    
                }
            }
        }
        
        public void Dispatch<T>(T message) where T : NetMessage {
            if (!MsgIds.ContainsKey(message.GetType())) {
                return;
            }
            
            NetworkServer.SendToAll(MsgIds[message.GetType()], message);
        }

        

        

        /*public void RemoveHandler<T>(NetworkMessageDelegate handler) where T : NetMessage {
            foreach (NetworkClient networkClient in NetworkClient.allClients) {
                if (Clients[networkClient].Contains(typeof(T))) {
                    Clients[networkClient].Remove(typeof(T));
                }
            }
        }*/

        /*public override void OnStartServer() {
           
            
        }*/

        private void AddClients() {
            
            for(int i = 0; i < NetworkClient.allClients.Count; i++) {
                if (!Clients.ContainsKey(NetworkClient.allClients[i])) {
                    Clients.Add(NetworkClient.allClients[i], new List<Type>());
                    if (i > 0) {
                        Clients[NetworkClient.allClients[i]] = Clients[NetworkClient.allClients[0]];
                    }
                }
                
            }

        }

    }

}
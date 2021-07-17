using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebPages;
using Microsoft.AspNet.SignalR;
using Google.Cloud.Speech.V1;

namespace PocketScribe.Controllers
{

    public class MyMessage
    {
        public string Msg { get; set; }
        public string Group { get; set; }
    }
    public class MyHub : Hub
    {
        private CancellationTokenSource cts;

        public async Task JoinRoom(string roomName)
        {
            await Groups.Add(Context.ConnectionId, roomName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.Remove(Context.ConnectionId, groupName);

        }

        public void Send(MyMessage message)
        {
            // Call the addMessage method on all clients            
            Clients.OthersInGroup(message.Group).MessageReceiver(message.Msg);
        }

        public void SendInterim(MyMessage message)
        {
            // Call the addMessage method on all clients            
            Clients.OthersInGroup(message.Group).MessageReceiverInterim(message.Msg);
        }


        public void BroadcastText(string connId, string message)
        {

            Clients.Client(connId).MessageReceiver(message);
            //Clients.Group(message.Group).MessageReceiver(message.Msg);
        }
    }
}
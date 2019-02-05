using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using UnitTestDemo.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace UnitTestDemo.Chat
{
    public class ChatHub : Hub
    {

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.            
            Clients.All.addNewMessageToPage(name, message);
        }

        private void SetUserConnection()
        {

        }

        #region Override

        public override async Task OnConnected()
        {
            var userName = Context.User.Identity.Name;
            var connecionId = Context.ConnectionId;

            var chatConnection = new ChatConnection
            {
                UserId = Context.User.Identity.GetUserId(),
                UserName = userName,
                ConnectionId = connecionId,
                Connected = true,
                ConnectedOn = DateTime.Now,
                UserAgent = Context.Request.Headers["User-Agent"]
            };

            var dbContext = ApplicationDbContext.Create();
            dbContext.ChatConnection.Add(chatConnection);
            await dbContext.SaveChangesAsync();


            Clients.AllExcept(connecionId).userConnected(userName, connecionId);
            //Clients.All.userConnected(userName, connecionId);
            await base.OnConnected();
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            var connecionId = Context.ConnectionId;
            var dbContext = ApplicationDbContext.Create();
            var chatConnection = await dbContext.ChatConnection.Where(x => x.ConnectionId == connecionId).FirstOrDefaultAsync();

            chatConnection.Connected = false;
            chatConnection.DisconectedOn = DateTime.Now;

            await dbContext.SaveChangesAsync();

            Clients.AllExcept(connecionId).userDisconnected(connecionId);

            await base.OnDisconnected(stopCalled);
        }

        #endregion
    }
}
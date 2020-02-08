using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;

namespace MakeIt.WebUI.SignalR.Hubs
{
    [HubName("notifyHub")]
    public class InviteNotificationHub : Hub
    {
        static List<User> Users = new List<User>();

        //static List<User> Users = new List<User>();

        //// Отправка сообщений
        //public void Send(string name, string message)
        //{
        //    Clients.All.addMessage(name, message);
        //}

        //// Подключение нового пользователя
        //public void Connect(string userName)
        //{
        //    var id = Context.ConnectionId;


        //    if (!Users.Any(x => x.ConnectionId == id))
        //    {
        //        Users.Add(new User { ConnectionId = id, Name = userName });

        //        // Посылаем сообщение текущему пользователю
        //        Clients.Caller.onConnected(id, userName, Users);

        //        // Посылаем сообщение всем пользователям, кроме текущего
        //        Clients.AllExcept(id).onNewUserConnected(id, userName);
        //    }
        //}

        //// Отключение пользователя
        //public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        //{
        //    var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
        //    if (item != null)
        //    {
        //        Users.Remove(item);
        //        var id = Context.ConnectionId;
        //        Clients.All.onUserDisconnected(id, item.Name);
        //    }

        //    return base.OnDisconnected(stopCalled);
        //}
    }
}
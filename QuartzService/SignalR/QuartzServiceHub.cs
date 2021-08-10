using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using QuartzServiceClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzService.SignalR
{
    public class QuartzServiceHub : Hub<IQuartzServiceHub>
    {
        private HubConnection _hubConnection;

        public async Task StartJobInScheduler(string job)
        {
            _hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:44308/hub", opt => opt.UseDefaultCredentials = true).WithAutomaticReconnect().Build();

            _hubConnection.On<string>("SendMessage", p => TestMethod(p));

            await _hubConnection.StartAsync();

            if (_hubConnection.State == HubConnectionState.Connected)
            {
                await _hubConnection.InvokeAsync<string>("SendCaller", job);

                var test = new QuartzJobStatus(2, TaskTypes.ЗагрузкаДиспетчерскогоБР, ConditionTypes.Задача_Запланирована, job);
                await Clients.Caller.ReturnStatusJob(test);
            }
        }

        private void TestMethod(string message)
        {
        }
    }
}
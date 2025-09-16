using Microsoft.AspNetCore.SignalR;

namespace AquaSolution.Server.SignalR
{
    public class SignalrHub :Hub
    {
        #region Menu và Role Admin
        public async Task ReloadMenuAsync()
        {
            await Clients.All.SendAsync("ReloadMenu");
        }
        #endregion
        #region RequestClinic
        public async Task LoadRequestClinic()
        {
            await Clients.All.SendAsync("LoadRequestClinic");
        }
        #endregion
        #region RequestSuport
        public async Task ChangeStatusRequestSuport()
        {
            await Clients.All.SendAsync("LoadRequestSuport");
        }
        #endregion
    }
}

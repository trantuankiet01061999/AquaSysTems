using AquaService.Shared.Permissions;
using AquaSolution.Client.Components.Users;
using AquaSolution.Shared.UserManagements;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace AquaSolution.Client.Pages.UserManagements
{
    public partial class Users
    {
        // Models

        [Inject] private HttpClient Http { get; set; }

        // State
        private List<UserDto> users = new();
        private string activeDropdown;
        private bool isLoading = true;

        // Dialog references
        private UserEditDialog userEditDialog;
        private RoleManagerDialog roleManagerDialog;
        //private DeleteConfirmDialog deleteConfirmDialog;

        // Current selected items
        private UserDto selectedUser;

        protected override async Task OnInitializedAsync()
        {
            await LoadUsers();
            isLoading = false;
        }

        private async Task LoadUsers()
        {
            try
            {
                users = await Http.GetFromJsonAsync<List<UserDto>>("api/user/mock-users");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading users: {ex.Message}");
            }
        }

        private void ToggleDropdown(string userId)
        {
            activeDropdown = activeDropdown == userId ? null : userId;
        }

        private void ShowAddUserDialog()
        {
            userEditDialog.Show(new UserDto());
        }

        private async Task EditUser(UserDto user)
        {
            userEditDialog.Show(user);
        }

        private async Task ShowRoleDialog(UserDto user)
        {

            selectedUser = user;
            roleManagerDialog.Show(user.Id, user.Roles);
        }

        private async Task ConfirmDelete(UserDto user)
        {
            selectedUser = user;
          //  deleteConfirmDialog.Show(user.UserName);
        }

        private async Task HandleUserSaved()
        {
            await LoadUsers();
        }

        private async Task HandleRolesSaved()
        {
            await LoadUsers();
        }

        private async Task HandleDeleteConfirmed()
        {
            await LoadUsers();
            selectedUser = null;
        }
    }
}

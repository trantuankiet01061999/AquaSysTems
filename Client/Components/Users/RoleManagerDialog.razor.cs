using AquaSolution.Shared.UserManagements;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace AquaSolution.Client.Components.Users
{
    public partial class RoleManagerDialog
    {
        [Parameter] public EventCallback OnSave { get; set; }

        // State
        private List<RoleDto> Roles { get; set; } = new();
        private bool IsVisible { get; set; }
        private bool IsLoading { get; set; }
        private bool HasError { get; set; }
        private string ErrorMessage { get; set; }
        private string UserId { get; set; }
        private string Username { get; set; }

        [Inject] private HttpClient Http { get; set; }

        public void Show(string userId, List<string> userRoles, string username = null)
        {
            UserId = userId;
            Username = username ?? "Người dùng";
            LoadRoles(userRoles);
            IsVisible = true;
            StateHasChanged();
        }

        private async void LoadRoles(List<string> userRoles)
        {
            IsLoading = true;
            HasError = false;

            try
            {
                // Load tất cả role từ API
                Roles = await Http.GetFromJsonAsync<List<RoleDto>>("api/roles/get-all");

                // Đánh dấu role nào user đang có
                foreach (var role in Roles)
                {
                    role.IsSelected = userRoles.Contains(role.Name);
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = "Không thể tải danh sách role";
                Console.WriteLine($"Error loading roles: {ex.Message}");
            }

            IsLoading = false;
            StateHasChanged();
        }

        private void ToggleRole(RoleDto role)
        {
            role.IsSelected = !role.IsSelected;
        }

        private void Close()
        {
            IsVisible = false;
            Roles = new List<RoleDto>();
            UserId = null;
            Username = null;
        }

        private async Task Save()
        {
            IsLoading = true;
            try
            {
                // Lấy danh sách role được chọn
                var selectedRoles = Roles
                    .Where(r => r.IsSelected)
                    .Select(r => r.Name)
                    .ToList();

                // Gọi API cập nhật
                var response = await Http.PostAsJsonAsync(
                    $"api/users/{UserId}/roles",
                    selectedRoles);

                if (response.IsSuccessStatusCode)
                {
                    Close();
                    await OnSave.InvokeAsync();
                }
                else
                {
                    ErrorMessage = "Cập nhật không thành công";
                    HasError = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Lỗi khi cập nhật role";
                HasError = true;
                Console.WriteLine($"Error saving roles: {ex.Message}");
            }

            IsLoading = false;
        }
    }
}



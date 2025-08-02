using AquaSolution.Client.Common;
using AquaSolution.Client.Components.ManageMedicalRooms.Products;
using AquaSolution.Shared.CommonDto;
using AquaSolution.Shared.ManageMedicalRooms.Products;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace AquaSolution.Client.Pages.ManageMedicalRooms.Productions
{
    public partial class Production
    {
        #region Declaration
        [Inject] private HttpClient Http { get; set; }
        private List<ProductDto> _products = new List<ProductDto>();
        private ProductModal ProductModal =new ProductModal();
        #endregion

        #region Innit
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
        private async Task LoadData()
        {
            _products = await Http.GetFromJsonAsync<List<ProductDto>>("api/product/get-all");
            await InvokeAsync(StateHasChanged);
        }
        #endregion
        #region Action 
        private async Task CreatedAsync()
        {
            await ProductModal.Showmodal( new ProductDto(),false);
        }

        private async Task DeleteAsync(ProductDto productDto)
        {
            var message = $"Bạn có muốn xóa product \" {productDto.Name} \" không?";
            var confirm = await MessageBox.Confirm(modal, message.ToString());
            if (confirm)
            {
                var response = await Http.DeleteAsync($"api/product/delete/{productDto.Id}");
                await LoadData();
                var content = await response.Content.ReadFromJsonAsync<ApiResponse>();
                if (response.IsSuccessStatusCode)
                {
                    await Message.Success(content?.message ?? "Xóa thành công");
                }
                else
                {
                    await Message.Error(content?.message ?? "Có lỗi xảy ra");
                }
            }
            await InvokeAsync(StateHasChanged);

        }
        private async Task EditAsync(ProductDto productDto)
        {
            await ProductModal.Showmodal(productDto, true);
        }
        #endregion

    }
}

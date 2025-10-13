using AquaSolution.Shared.CommonDto;
using System.Net.Http.Json;
using System.Text;

namespace AquaSolution.Client.Common.SendEmailHelper
{
    public static class SendEmailRequestClinic
    {
        private static async Task HandleEmail(SendEmailDto sendEmailDto)
        {
            var url = "https://prod-15.southeastasia.logic.azure.com:443/workflows/2dcf2a5ca6a74e7ca0565bc92a03c5b1/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=wVvZUqoCZW7gvxjgfiLGLJHdZDndFg9oeOKjGxquIKM";
            var payload = new
            {
                Id = sendEmailDto.RequestId,
                Status = "No Status",
                To = sendEmailDto.ToAdress,
                sendEmailDto.Subject,
                Body = sendEmailDto.Body.ToString()
            };

            using var client = new HttpClient();
            var response = await client.PostAsJsonAsync(url, payload);
        }
        public static async Task SendEmailRequestAsync(string emailManager)
        {
            var data = await EmailTemplate(emailManager);
            // await HandleEmail(data);
        }
        private static Task<SendEmailDto> EmailTemplate(string emailManager)
        {
            var sendEmailDto = new SendEmailDto();
            var bodyEmail = new StringBuilder();
            sendEmailDto.RequestId = Guid.NewGuid();
            sendEmailDto.ToAdress = emailManager;
            bodyEmail.AppendLine($"<strong>Bạn có 1 yêu cầu cần được Approval.</strong>");
            bodyEmail.AppendLine("</ul>");
            bodyEmail.AppendLine("<p><a href='http://10.56.1.14/AquaSolution/'>Vào To do list Xem chi tiết </a></p>");
            bodyEmail.AppendLine("<p>nếu dùng điện thoại thì xoay ngang màn hình</p>");
            sendEmailDto.Body = bodyEmail;
            return Task.FromResult(sendEmailDto);
        }
    }
}

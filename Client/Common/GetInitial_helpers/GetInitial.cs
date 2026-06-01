namespace AquaSolution.Client.Common.GetInitial_helpers
{
    public class GetInitial
    {
        // RequestITSuport.razor.cs
        // Thêm helper method GetInitials vào code-behind hoặc @code block của bạn

        /// <summary>
        /// Lấy 2 chữ cái đầu của tên để hiển thị avatar
        /// Ví dụ: "Nguyễn Văn A" => "NV"
        /// </summary>
        public string GetInitials(string? fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return "?";
            var parts = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1) return parts[0][..1].ToUpper();
            return $"{parts[0][0]}{parts[^1][0]}".ToUpper();
        }
        public  string GetAvatarColor(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "#1677ff";
            string[] colors = new[]
            {
                    "#1677ff", "#52c41a", "#fa8c16", "#f5222d",
                    "#722ed1", "#13c2c2", "#eb2f96", "#fa541c",
                    "#2f54eb", "#08979c"
                };
            int index = Math.Abs(name.GetHashCode()) % colors.Length;
            return colors[index];
        }

    }
}

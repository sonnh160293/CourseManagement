namespace DTO.GetDTO
{
    public class AccountGetDTO
    {
        public int AccountId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public bool? StatusId { get; set; }

        public RoleDTO? Role { get; set; }
    }
}

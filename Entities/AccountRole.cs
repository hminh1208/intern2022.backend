namespace WebApi.Entities
{
    public class AccountRole
    {
        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}

namespace SBASeatingPlan.Admin
{
    internal record UserModel(string EntryCode)
    {
        public string Email { get; set; } = string.Empty;
    }
}

namespace Application.Futures.Staff.Dtos
{
    public  record  StaffDto
    {
        public string?  Fullname { get; set; }
        public string?  Username { get; set; }
        public string? IsActiveStaff { get; set; }
        public string? OrganizationName { get; set; }
        public string? IsActiveOrganization { get; set; }
    }
}

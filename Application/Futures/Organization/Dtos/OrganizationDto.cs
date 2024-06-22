namespace Application.Futures.Organization.Dtos
{
    public  record  OrganizationDto
    {
        public string  Name  { get; set; }
        public virtual ICollection<StaffDto> Staffs { get; set; }

    }


    public record  StaffDto 
    {
        public string Fullname { get; set; }
        public string Username { get; set; }

    }
}

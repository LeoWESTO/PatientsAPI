namespace PatientsAPI.Database.Models
{
    public class Patient : BaseEntity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public Guid AreaId { get; set; }
        public Area Area { get; set; }
    }
}

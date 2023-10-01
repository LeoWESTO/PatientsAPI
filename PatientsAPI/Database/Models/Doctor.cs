namespace PatientsAPI.Database.Models
{
    public class Doctor : BaseEntity
    {
        public string FullName { get; set; }
        public Guid CabinetId { get; set; }
        public Cabinet Cabinet { get; set; }
        public Guid SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public Guid? AreaId { get; set; }
        public Area? Area { get; set; }
    }
}

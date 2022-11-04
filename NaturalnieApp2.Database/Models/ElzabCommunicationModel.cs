namespace NaturalnieApp2.Database.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("elzab_communication")]
    public class ElzabCommunicationModel
    {
        public enum CommunicationStatus
        {
            Started,
            FinishSuccess,
            FinishFailed,
        }

        [Key]
        public int Id { get; set; }
        public DateTime DateOfCommunication { get; set; }
        public CommunicationStatus StatusOfCommunication { get; set; }
        public string ElzabCommandName { get; set; }
        public int ElzabCommandReportStatusCode { get; set; }
        public string ElzabCommandReportStatusText { get; set; }

    }
}

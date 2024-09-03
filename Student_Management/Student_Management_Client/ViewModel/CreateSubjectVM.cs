using System.ComponentModel.DataAnnotations;

namespace Student_Management_Client.ViewModel
{
    public class CreateSubjectVM
    {
        [Required(ErrorMessage = "Subject Name is required.")]
        public string? SubjectName { get; set; }

        [Required(ErrorMessage = "Subject Code is required.")]
        public string? SubjectCode { get; set; }

        [Required(ErrorMessage = "Number of Credits is required.")]
        public int? NumOfCredit { get; set; }

        public int? SubjectPrequisite { get; set; }

        [Required(ErrorMessage = "Major is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please choose major")]
        public int? MajorId { get; set; }


        [Required(ErrorMessage = "Term is required.")]
        public int? Term { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public bool? Status { get; set; }


        public int? CreatedBy { get; set; }
    }

}

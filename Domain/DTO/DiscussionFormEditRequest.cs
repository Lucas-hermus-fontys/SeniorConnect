using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class DiscussionFormEditRequest
    {
        [Required(ErrorMessage = "Minimaal 1 character is verplicht")]
        [Length(1, 200, ErrorMessage = "Een titel mag slechts 200 characters lang zijn")]
        public string Text { get; set; }
        
        [Required(ErrorMessage = "Er is iets mis gegaan")]
        public int DiscussionFormId { get; set; }
        
        public int ParentId { get; set; }
    }
}

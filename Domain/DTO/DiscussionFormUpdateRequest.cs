using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class DiscussionFormUpdateRequest
    {
        [Required(ErrorMessage = "Een titel is verplicht")]
        [Length(1, 100, ErrorMessage = "Een titel mag slechts 100 characters lang zijn")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Een omschrijving is verplicht")]
        [Length(1, 500, ErrorMessage = "Een omschrijving mag slechts 500 characters lang zijn")]
        public string Description { get; set; }
        
        public int Id { get; set; }
    }
}

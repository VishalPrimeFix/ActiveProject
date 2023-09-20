using System.ComponentModel.DataAnnotations;

namespace NodeProject.Models
{
    public class NodeViewModel
    {
        [Required]
        [MaxLength]
        public string NodeName { get; set; }

        [Required]
        public string ParentNodeName { get; set; }

        public int ParentNodeId { get; set; }
    }
}

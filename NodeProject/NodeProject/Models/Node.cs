using System.ComponentModel.DataAnnotations;

namespace NodeProject.Models
{
    public class Node
    {
        [Key]
        public int NodeId { get; set; }

        [Required]
        [MaxLength]
        public string NodeName { get; set; }

        public int ParentNodeId { get; set; } = 0;

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; } = DateTime.Now;
    }
}

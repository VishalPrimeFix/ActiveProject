namespace NodeProject.Models
{
    public class NodeCreateViewModel
    {
        public NodeViewModel NodeViewModel { get; set; }

        public List<NodeView> NodeViews { get; set; }

    }
    public class NodeUpdateViewModel : NodeCreateViewModel
    { 
        public int Id { get; set; }
    }
}

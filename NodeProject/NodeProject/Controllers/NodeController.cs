using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NodeProject.Models;

namespace NodeProject.Controllers
{
    public class NodeController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;

        public NodeController(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var items = _dataBaseContext.Nodes.ToList();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            var nodeNameList = _dataBaseContext.Nodes.Where(x => x.ParentNodeId > 0).Select(x => new NodeView { NodeName = x.NodeName, NodeId = x.NodeId}).ToList();
            var node = new NodeCreateViewModel();
            node.NodeViews = nodeNameList;
            return View(node);
        }

        [HttpPost]
        public IActionResult Create(NodeCreateViewModel viewModel)
        {
            var newNode = new Node
            {
                NodeName = viewModel.NodeViewModel.NodeName,
                ParentNodeId = viewModel.NodeViewModel.ParentNodeId,
                IsActive = true
            };
            _dataBaseContext.Nodes.Add(newNode);
            _dataBaseContext.SaveChanges();
            return RedirectToAction("Success");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var createNode = new NodeUpdateViewModel();
            var nodeNameList = _dataBaseContext.Nodes.Where(x => x.ParentNodeId > 0).Select(x => new NodeView { NodeName = x.NodeName, NodeId = x.NodeId }).ToList();

            if (id == null || _dataBaseContext.Nodes == null)
            {
                return NotFound();
            }

            var node = await _dataBaseContext.Nodes.FindAsync(id);
            if (node == null)
            {
                return NotFound();
            }
            createNode.NodeViews = nodeNameList;
            createNode.NodeViewModel = new NodeViewModel();
            createNode.NodeViewModel.NodeName = node.NodeName;
            createNode.Id = node.NodeId;
            return View(createNode);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NodeUpdateViewModel nodeCreate)
        {
            var node = await _dataBaseContext.Nodes.FindAsync(nodeCreate.Id);

            if (node == null)
            {
                return NotFound();
            }

            node.NodeName = nodeCreate.NodeViewModel.NodeName;
            node.ParentNodeId = nodeCreate.NodeViewModel.ParentNodeId;

            _dataBaseContext.Nodes.Update(node);
            await _dataBaseContext.SaveChangesAsync();

            return RedirectToAction("Success");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _dataBaseContext.Nodes == null)
            {
                return NotFound();
            }

            var node = await _dataBaseContext.Nodes
                .FirstOrDefaultAsync(m => m.NodeId == id);
            if (node == null)
            {
                return NotFound();
            }

            return View(node);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_dataBaseContext.Nodes == null)
            {
                return Problem("Entity set 'DataBaseContext.Nodes'  is null.");
            }
            var node = await _dataBaseContext.Nodes.FindAsync(id);
            if (node != null)
            {
                _dataBaseContext.Nodes.Remove(node);
            }

            await _dataBaseContext.SaveChangesAsync();
            return RedirectToAction("Success");
        }

        [HttpGet]
        public IActionResult NodeTree()
        {
            var result = _dataBaseContext.Nodes.Where(x => x.ParentNodeId == 0).ToList();
            var r = new List<TreeModel>();

            foreach (var item in result)
            {
                var tree = new TreeModel();
                tree.ParentNode = item.NodeName;
                var re = _dataBaseContext.Nodes.Where(x => x.ParentNodeId == item.NodeId).Select(x => x.NodeName).ToList();
                tree.ChildNode = re;
                r.Add(tree);
            }
            
            return View(r);
        }
    }
}

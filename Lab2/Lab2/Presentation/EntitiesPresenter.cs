using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2.Presentation
{
    class EntitiesPresenter
    {
        private readonly TreeView _treeView;
        private readonly Func<IEnumerable<IEntity>> _getEntities;
        public EntitiesPresenter(TreeView treeView, Func<IEnumerable<IEntity>> getEntities)
        {
            _treeView = treeView;
            _getEntities = getEntities;
            Documento.GetInstance().Changed += DocumentoChanged;
            DocumentoChanged(Documento.GetInstance(), EventArgs.Empty);
        }

        private void DocumentoChanged(object sender, EventArgs e)
        {
            Populate(_getEntities());
        }

        private void Populate(IEnumerable<IEntity> entities)
        {
            _treeView.Nodes.Clear();
            foreach (IEntity entity in entities)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = entity.FullName;
                treeNode.BackColor = entity.BackColor;
                treeNode.ForeColor = entity.ForeColor;
                _treeView.Nodes.Add(treeNode);
                foreach(IEntity subEntity in entity.SubEntities)
                {
                    TreeNode subTreeNode = new TreeNode();
                    subTreeNode.Text = subEntity.FullName;
                    subTreeNode.BackColor = subEntity.BackColor;
                    subTreeNode.ForeColor = subEntity.ForeColor;
                    treeNode.Nodes.Add(subTreeNode);
                }
            }
            _treeView.ExpandAll();
        }
    }
}

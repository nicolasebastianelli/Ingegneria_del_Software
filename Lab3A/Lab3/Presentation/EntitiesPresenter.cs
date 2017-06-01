using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

using Lab3.Model;

namespace Lab3.Presentation
{
    class EntitiesPresenter
    {
        //  View passiva gestita dal presenter
        private readonly TreeView _treeView;
        private readonly Documento _documento;
        //  Delegate che incapsula il metodo da invocare per ottenere le entities da visualizzare
        private readonly Func<IEnumerable<IEntity>> _getEntities;

        public EntitiesPresenter(TreeView treeView, Documento documento, Func<IEnumerable<IEntity>> getEntities)
        {
            if (treeView == null)
                throw new ArgumentNullException("treeView");
            if (documento == null)
                throw new ArgumentNullException("documento");
            if (getEntities == null)
                throw new ArgumentNullException("getEntities");
            _treeView = treeView;
            _documento = documento;
            _getEntities = getEntities;
            _documento.Changed += DocumentoChanged;
            DocumentoChanged(_documento, EventArgs.Empty);
        }

        private void DocumentoChanged(object sender, EventArgs e)
        {
            Populate(_getEntities());
        }

        private void Populate(IEnumerable<IEntity> entities)
        {
            _treeView.Nodes.Clear();
            Populate(_treeView.Nodes, entities, 2);
            _treeView.Sort();
            _treeView.ExpandAll();
        }

        private void Populate(TreeNodeCollection nodes, IEnumerable<IEntity> entities, int level)
        {
            if (level == 0)
                return;
            foreach (IEntity entity in entities)
            {
                TreeNode treeNode = new TreeNode(entity.FullName);
                treeNode.BackColor = entity.BackColor;
                treeNode.ForeColor = entity.ForeColor;
                nodes.Add(treeNode);
                Populate(treeNode.Nodes, entity.SubEntities, level - 1);
            }
        }
    }
}

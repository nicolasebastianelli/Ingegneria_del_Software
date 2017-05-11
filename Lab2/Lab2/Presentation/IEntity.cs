using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Presentation
{
    interface IEntity
    {
        string FullName { get; }
        Color BackColor { get; }
        Color ForeColor { get; }
        IEnumerable<IEntity> SubEntities { get; }
    }
}

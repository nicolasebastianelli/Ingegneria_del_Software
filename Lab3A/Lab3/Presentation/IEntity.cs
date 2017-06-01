using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.Generic;

namespace Lab3.Presentation
{
    interface IEntity
    {
        string FullName { get; }
        Color BackColor { get; }
        Color ForeColor { get; }
        IEnumerable<IEntity> SubEntities { get; }
    }
}

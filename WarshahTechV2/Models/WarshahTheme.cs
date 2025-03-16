using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class WarshahTheme
    {
        public WarshahTheme()
        {
            Warshahs = new HashSet<Warshah>();
        }

        public Guid Id { get; set; }
        public string Header { get; set; }
        public string Sidebare { get; set; }
        public string MainIcons { get; set; }
        public string BackgroundColor { get; set; }
        public string SidebareText { get; set; }
        public string SidebareActiveText { get; set; }

        public virtual ICollection<Warshah> Warshahs { get; set; }
    }
}

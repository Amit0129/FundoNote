using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class AddNoteModel
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime RemindMe { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool IsAechive { get; set; }
        public bool IsPin { get; set; }
        public bool IsTrash { get; set; }
    }
}

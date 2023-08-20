using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepoLayer.Entities
{
    public class NotesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteId { get; set; }
        public string? Title { get; set; }
        public string? Note { get; set; }
        public DateTime? RemindMe { get; set; }
        public string? Color { get; set; }
        public string? Image { get; set; }
        public bool IsAechive { get; set; }
        public bool IsPin { get; set; }
        public bool IsTrash { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Edited { get; set; }

        [ForeignKey("Users")]
        public long UserId { get; set; }   
        public virtual UserEntity user { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace RepoLayer.Entities
{
    public class CollaboratorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int collaboratorId { get; set; }
        public string CollabEmail { get; set; }
        [ForeignKey("Users")]
        public long? UserId { get; set; }
        [JsonIgnore]
        public virtual UserEntity user { get; set; }
        [ForeignKey("Notes")]
        public long? NoteId { get; set; }
        [JsonIgnore]
        public virtual NotesEntity Note { get; set; }
    }
}

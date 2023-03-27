using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Supabase;
using Supabase.Realtime;
using Supabase.Realtime.Socket;
using System.Text;
using System.Threading.Tasks;
using Supabase;
using Postgrest.Attributes;
using Postgrest.Models;

namespace LW_4
{
    [Table("Messages")]
    public class Message : BaseModel
    {
        [Column("email_user")] public string Email { get; set; }
        [Column("message")] public string Text { get; set; }

        public override string ToString()
        {
            return $"{Email} - {Text}";
        }
    }
}

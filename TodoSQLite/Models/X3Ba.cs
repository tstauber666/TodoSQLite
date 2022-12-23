using System;
using System.Collections.Generic;
using SQLite;
namespace TodoSQLite.Models;
//using SQLiteNetExtensions;
//using SQLiteNetExtensions.Attributes;

public  class X3Ba
{
    public long Icode { get; set; }
    //[OneToMany(CascadeOperations = CascadeOperation.All)]      // One to many relationship with Valuation
    //public List<AaaTestBaeume> BObjects { get; set; }

    public string KurzD { get; set; } = null!;

    public string LangD { get; set; } = null!;

    //public virtual ICollection<AaaTestBaeume> AaaTestBaeumes { get; } = new List<AaaTestBaeume>();
}

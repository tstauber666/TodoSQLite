using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using SQLite;

namespace TodoSQLite.Models;

public  class AaaTestBaeume
{
    [SQLite.PrimaryKey,SQLite.AutoIncrementAttribute, SQLite.Column("Id")]
    public long Id { get; set; }

    public string Dathoheit { get; set; }

    public long? Tnr { get; set; }

    public long? Enr { get; set; }

    public long Bnr { get; set; }

    //[ForeignKey(typeof(X3Ba))] 
    public long Ba { get; set; }

    public long Bhd { get; set; }

    public long Hoehe { get; set; }

    //[ManyToOne]      // Many to one relationship with X3Ba
    //public X3Ba X3Ba { get; set; }
    // public virtual X3Ba BaNavigation { get; set; } = null!;
    //public  ObservableCollection<X3Ba> BaumListe { get; set; } 
    //public List<string> BaumListe = new() { "Kiefer", "Fichte" };
}

public class BaumartenListe:AaaTestBaeume
{
    public ObservableCollection<X3Ba> BaumListe { get; set; }
}

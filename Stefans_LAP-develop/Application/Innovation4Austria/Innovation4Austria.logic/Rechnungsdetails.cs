
//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Innovation4Austria.logic
{

using System;
    using System.Collections.Generic;
    
public partial class Rechnungsdetails
{

    public int Id { get; set; }

    public int Rechnung_Id { get; set; }

    public int Buchungsdetail_Id { get; set; }



    public virtual Buchungsdetails Buchungsdetails { get; set; }

    public virtual Rechnung Rechnung { get; set; }

}

}

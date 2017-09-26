using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.AppCode
{
    public class Validation
    {
        public const string REQUIRED = "Required";
        public const string DATATYPE_MAIL = "DataTypeMail";
        public const string DATATYPE_DATE = "DataTypeDate";
        public const string REGULAR_EXPRESSION_PASSWORD = "RegexPassword";
        public const string MAXLENGTH = "MaxLength";
        public const string NAMENSKONTROLLE = "NamensKontrolle";
        public const string PASSWORT_WIEDERHOLEN = "Passwortwiederholung nicht gleich Passwort";
        public const string PASSWORT_NORM = "StringLength";

        public const string INNOVATION4AUSTRIAMITARBEITER = "MitarbeiterIVA";
        public const string FIRMENANSPRECHPARTNER = "Firmenansprechpartner";

        public const string FIRMAANLEGENSUCCESS = "FirmaAnlegegSuccess";
        public const string FIRMAANLEGENERROR = "FirmaAnlegenError";

        public const string DATUMVORBEI = "UngueltigesDatum";

        public const string POSTLEITZAHL = "Postleitzahl";

    }
}
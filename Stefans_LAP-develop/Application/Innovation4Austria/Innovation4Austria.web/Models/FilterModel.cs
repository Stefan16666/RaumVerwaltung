using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Innovation4Austria.web.Models
{
    public class FilterModel
    {
       
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BeginnDatum { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDatum { get; set; }

        public List<RaumAusstattungsModel> Ausstattung { get; set; }

        public int[] Ausstattungs_id { get; set; }

        public List<RaumArtModel> Art { get; set; }

        public int Art_id { get; set; }

        public RaumBuchungsModel  gebuchteRaeume { get; set; }
    }
}
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;
//Reference not found 
//In order to use [AllowHtml] 

namespace Iclic.Plugin.Widgets.BlocPub.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }


        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Picture")]
        [UIHint("Picture")]
        public int Picture1Id { get; set; }
        public bool Picture1Id_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Nom")]
        //[AllowHtml]
        public string Nom1 { get; set; }
        public bool Nom1_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Link")]
        //[AllowHtml]
        public string Url1 { get; set; }
        public bool Url1_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Tag")]
        //[AllowHtml]
        public int Tag1 { get; set; }
        public bool Tag1_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateDebut")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateDebut1 { get; set; }
        public bool DateDebut1_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateFin")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateFin1 { get; set; }
        public bool DateFin1_OverrideForStore { get; set; }




        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Picture")]
        [UIHint("Picture")]
        public int Picture2Id { get; set; }
        public bool Picture2Id_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Nom")]
        //[AllowHtml]
        public string Nom2 { get; set; }
        public bool Nom2_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Link")]
        //[AllowHtml]
        public string Url2 { get; set; }
        public bool Url2_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Tag")]
        //[AllowHtml]
        public int Tag2 { get; set; }
        public bool Tag2_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateDebut")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateDebut2 { get; set; }
        public bool DateDebut2_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateFin")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateFin2 { get; set; }
        public bool DateFin2_OverrideForStore { get; set; }



        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Picture")]
        [UIHint("Picture")]
        public int Picture3Id { get; set; }
        public bool Picture3Id_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Nom")]
        //[AllowHtml]
        public string Nom3 { get; set; }
        public bool Nom3_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Link")]
        //[AllowHtml]
        public string Url3 { get; set; }
        public bool Url3_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Tag")]
        //[AllowHtml]
        public int Tag3 { get; set; }
        public bool Tag3_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateDebut")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateDebut3 { get; set; }
        public bool DateDebut3_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateFin")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateFin3 { get; set; }
        public bool DateFin3_OverrideForStore { get; set; }



        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Picture")]
        [UIHint("Picture")]
        public int Picture4Id { get; set; }
        public bool Picture4Id_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Nom")]
        //[AllowHtml]
        public string Nom4 { get; set; }
        public bool Nom4_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Link")]
        //[AllowHtml]
        public string Url4 { get; set; }
        public bool Url4_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Tag")]
        //[AllowHtml]
        public int Tag4 { get; set; }
        public bool Tag4_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateDebut")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateDebut4 { get; set; }
        public bool DateDebut4_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateFin")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateFin4 { get; set; }
        public bool DateFin4_OverrideForStore { get; set; }



        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Picture")]
        [UIHint("Picture")]
        public int Picture5Id { get; set; }
        public bool Picture5Id_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Nom")]
        //[AllowHtml]
        public string Nom5 { get; set; }
        public bool Nom5_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Link")]
        //[AllowHtml]
        public string Url5 { get; set; }
        public bool Url5_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Tag")]
        //[AllowHtml]
        public int Tag5 { get; set; }
        public bool Tag5_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateDebut")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateDebut5 { get; set; }
        public bool DateDebut5_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateFin")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateFin5 { get; set; }
        public bool DateFin5_OverrideForStore { get; set; }
 

        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Picture")]
        [UIHint("Picture")]
        public int Picture6Id { get; set; }
        public bool Picture6Id_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Nom")]
        //[AllowHtml]
        public string Nom6 { get; set; }
        public bool Nom6_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Link")]
        //[AllowHtml]
        public string Url6 { get; set; }
        public bool Url6_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Tag")]
        //[AllowHtml]
        public int Tag6 { get; set; }
        public bool Tag6_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateDebut")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateDebut6 { get; set; }
        public bool DateDebut6_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateFin")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateFin6 { get; set; }
        public bool DateFin6_OverrideForStore { get; set; }


        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Picture")]
        [UIHint("Picture")]
        public int Picture7Id { get; set; }
        public bool Picture7Id_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Nom")]
        //[AllowHtml]
        public string Nom7 { get; set; }
        public bool Nom7_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Link")]
        //[AllowHtml]
        public string Url7 { get; set; }
        public bool Url7_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Tag")]
        //[AllowHtml]
        public int Tag7 { get; set; }
        public bool Tag7_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateDebut")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateDebut7 { get; set; }
        public bool DateDebut7_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateFin")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateFin7 { get; set; }
        public bool DateFin7_OverrideForStore { get; set; }


        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Picture")]
        [UIHint("Picture")]
        public int Picture8Id { get; set; }
        public bool Picture8Id_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Nom")]
        //[AllowHtml]
        public string Nom8 { get; set; }
        public bool Nom8_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Link")]
        //[AllowHtml]
        public string Url8 { get; set; }
        public bool Url8_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Tag")]
        //[AllowHtml]
        public int Tag8 { get; set; }
        public bool Tag8_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateDebut")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateDebut8 { get; set; }
        public bool DateDebut8_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateFin")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateFin8 { get; set; }
        public bool DateFin8_OverrideForStore { get; set; }


        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Picture")]
        [UIHint("Picture")]
        public int Picture9Id { get; set; }
        public bool Picture9Id_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Nom")]
        //[AllowHtml]
        public string Nom9 { get; set; }
        public bool Nom9_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Link")]
        //[AllowHtml]
        public string Url9 { get; set; }
        public bool Url9_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Tag")]
        //[AllowHtml]
        public int Tag9 { get; set; }
        public bool Tag9_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateDebut")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateDebut9 { get; set; }
        public bool DateDebut9_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateFin")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateFin9 { get; set; }
        public bool DateFin9_OverrideForStore { get; set; }


        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Picture")]
        [UIHint("Picture")]
        public int Picture10Id { get; set; }
        public bool Picture10Id_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Nom")]
        //[AllowHtml]
        public string Nom10 { get; set; }
        public bool Nom10_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Link")]
        //[AllowHtml]
        public string Url10 { get; set; }
        public bool Url10_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.Tag")]
        //[AllowHtml]
        public int Tag10 { get; set; }
        public bool Tag10_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateDebut")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateDebut10 { get; set; }
        public bool DateDebut10_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BlocPub.DateFin")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateFin10 { get; set; }
        public bool DateFin10_OverrideForStore { get; set; }
    }
}
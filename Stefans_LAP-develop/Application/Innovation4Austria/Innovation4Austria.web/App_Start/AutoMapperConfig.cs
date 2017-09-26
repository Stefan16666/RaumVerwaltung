using AutoMapper;
using innovation4austria;
using Innovation4Austria.logic;
using Innovation4Austria.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace innovation4austria.web
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<Benutzer, PasswortVerwaltungsModel>()
                .ForMember(dest => dest.NeuesPasswort, opts => opts.Ignore())
                .ForMember(dest => dest.NeuesPasswortBestätigung, opts => opts.Ignore())
                .ForMember(dest => dest.Passwort, opts => opts.MapFrom(source => source.Passwort));

                x.CreateMap<Benutzer, BenutzerVerwaltungsModel>()
                .ForMember(dest => dest.FirmenName, opts => opts.MapFrom(source => source.Firma.Bezeichnung))
                .ForMember(dest => dest.FirmenName, opts => opts.NullSubstitute("Innovation  4 Austria"))
                .ForMember(dest => dest.Rolle, opts => opts.MapFrom(source => source.Rolle.Bezeichnung))
                 .ForMember(dest => dest.Fa_id, opts => opts.Ignore());

                x.CreateMap<Firma, FirmenModel>()
                .ForMember(dest => dest.NeuerBenutzer, opts => opts.Ignore());

                x.CreateMap<BenutzerVerwaltungsModel, Benutzer>()
                 .ForMember(dest => dest.Firma, opts => opts.Ignore())
                 .ForMember(dest => dest.Passwort, opts => opts.Ignore())
                 .ForMember(dest => dest.Firma_id, opts => opts.Ignore())
                 .ForMember(dest => dest.Rolle_id, opts => opts.Ignore())
                 .ForMember(dest => dest.Aktiv, opts => opts.Ignore())
                 .ForMember(dest => dest.AlleLog, opts => opts.Ignore())
                 .ForMember(dest => dest.Id, opts => opts.Ignore())
                 .ForMember(dest => dest.Rolle, opts => opts.Ignore())
                 ;

                x.CreateMap<Benutzer, BenutzerModel>()
                .ForMember(dest => dest.Fa_id, opts => opts.Ignore());

                x.CreateMap<Raum, RaumModel>()
                .ForMember(dest => dest.RaumArt, opts => opts.MapFrom(source => source.Art.Bezeichnung))
                ;

                x.CreateMap<Ausstattung, RaumAusstattungsModel>()
                .ForMember(dest => dest.Bezeichnung, opts => opts.MapFrom(source => source.Bezeichnung))
                .ForAllMembers(dest => dest.Ignore())
                ;

                x.CreateMap<Art, RaumArtModel>();

                x.CreateMap<Raum, BuchungsDetailModel>()
                .ForMember(dest => dest.RaumArt, opts => opts.MapFrom(source => source.Art.Bezeichnung))
                .ForMember(dest => dest.VonDatum, opts => opts.Ignore())
                .ForMember(dest => dest.BisDatum, opts => opts.Ignore())
                .ForMember(dest => dest.RaumAusstattung, opts => opts.Ignore())
                .ForMember(dest => dest.Firma, opts => opts.Ignore())
                .ForMember(dest => dest.Fa_Id, opts => opts.Ignore());

                x.CreateMap<Firma, FirmenAusWahlModel>();


                x.CreateMap<Raum_Ausstattung, RaumAusstattungsFilterModel>()
                .ForMember(dest => dest.Bezeichnung, opts => opts.MapFrom(source => source.Ausstattung.Bezeichnung))
                .ForMember(dest => dest.Ausstattungs_Id, opts => opts.MapFrom(source => source.Ausstattungs_Id))
                ;

                x.CreateMap<FirmaAnlegenModel, Firma>()
                .ForMember(dest => dest.Bezeichnung, opts => opts.MapFrom(source => source.Bezeichnung))
                .ForMember(dest => dest.Nummer, opts => opts.MapFrom(source => source.Nummer))
                .ForMember(dest => dest.Strasse, opts => opts.MapFrom(source => source.Strasse))
                .ForMember(dest => dest.Plz, opts => opts.MapFrom(source => source.Plz))
                .ForMember(dest => dest.Ort, opts => opts.MapFrom(source => source.Ort))
                .ForAllMembers(dest => dest.Ignore());

            });

            Mapper.AssertConfigurationIsValid();


        }
    }
}
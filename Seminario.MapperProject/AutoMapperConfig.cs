using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Mappers;
using AutoMapper;
using Seminario.Model;
using Seminario.Dto;

namespace Seminario.MapperProject
{
    public static class AutoMapperConfig
    {
        public static IMapper mapper;
        public static void RegisterAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                
                cfg.CreateMap<Simulacion, SimulacionDto>()
                .ForMember(dto => dto.TipoCateg, model => model.MapFrom(m => m.TipoCategoria))
                .ForMember(dto => dto.Cheques, model => model.MapFrom(s => s.Cheques.ToArray()));
                
                cfg.CreateMap<SimulacionDto,Simulacion>()
                    .ForMember(model => model.Provincia, dto => dto.Ignore())
                    .ForMember(model => model.Producto, dto => dto.Ignore())
                    .ForMember(model => model.Empleado, dto => dto.Ignore())
                    .ForMember(model => model.Cheques, dto => dto.Ignore());

                cfg.CreateMap<Cheque, ChequeDto>()
                    .ForMember(dto => dto.Documento, model => model.MapFrom(m => m.CuitEmisor))
                    .ForMember(dto => dto.Nosis, model => model.MapFrom(m =>m.EstadoNosisEmisor))
                    .ForMember(dto => dto.TEOps, model => model.MapFrom(m => m.TE))
                    .ForMember(dto => dto.TEAdelantada, model => model.MapFrom(m => m.TEA))
                    .ForMember(dto => dto.Ponderado, model => model.MapFrom(m => m.ImportePonderado));
                
                cfg.CreateMap<ChequeDto, Cheque>()
                    .ForMember(m => m.CuitEmisor, model => model.MapFrom(dto => dto.Documento))
                    .ForMember(m => m.EstadoNosisEmisor, model => model.MapFrom(dto => dto.Nosis))
                    .ForMember(m => m.TE, model => model.MapFrom(dto => dto.TEOps))
                    .ForMember(m => m.TEA, model => model.MapFrom(dto => dto.TEAdelantada))
                    .ForMember(m => m.ImportePonderado, model => model.MapFrom(dto => dto.Ponderado));

                cfg.CreateMap<Provincia, ProvinciaDto>();

                cfg.CreateMap<ProvinciaDto, Provincia>();

                cfg.CreateMap<Producto, ProductoDto>();

                cfg.CreateMap<ProductoDto, Producto>();

                cfg.CreateMap<DatosTTDto, DatosTT>();
                cfg.CreateMap<DatosTT, DatosTTDto>();
            });
            mapper = new Mapper(config);
        }
       
    }
}

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
                .ForMember(dto => dto.ValorNominal, model => model.MapFrom(m => m.ImporteTotal))
                .ForMember(dto => dto.Intereses, model => model.MapFrom(m => m.InteresTotal))
                .ForMember(dto => dto.Comision, model => model.MapFrom(m => m.ComisionTotal))
                .ForMember(dto => dto.Sellado, model => model.MapFrom(m => m.SelladoTotal))
                .ForMember(dto => dto.Iva, model => model.MapFrom(m => m.IvaTotal))
                .ForMember(dto => dto.NetoLiquidar, model => model.MapFrom(m => m.NetoLiquidarTotal))
                .ForMember(dto => dto.TipoCateg, model => model.MapFrom(m => m.TipoCategoria))
                .ForMember(dto => dto.CodProd, model => model.MapFrom(m => m.Producto.Id))
                .ForMember(dto => dto.Legajo, model => model.MapFrom(m => m.Empleado.EmpleadoId))
                .ForMember(dto => dto.Cheques, model => model.MapFrom(m => m.Cheques.Select(c => c.Id)))
                .ForMember(dto => dto.IdProvincia, model => model.MapFrom(m => m.Provincia));
                
                
                cfg.CreateMap<SimulacionDto,Simulacion>()
                    .ForMember(model => model.Provincia, dto => dto.Ignore())
                    .ForMember(model => model.Producto, dto => dto.Ignore())
                    .ForMember(model => model.Empleado, dto => dto.Ignore())
                    .ForMember(model => model.Cheques, dto => dto.Ignore())
                    .ForMember(m => m.ImporteTotal, model => model.MapFrom(dto => dto.ValorNominal))
                    .ForMember(m => m.InteresTotal, model => model.MapFrom(dto => dto.Intereses))
                    .ForMember(m => m.ComisionTotal, model => model.MapFrom(dto => dto.Comision))
                    .ForMember(m => m.SelladoTotal, model => model.MapFrom(dto => dto.Sellado))
                    .ForMember(m => m.IvaTotal, model => model.MapFrom(dto => dto.Iva))
                    .ForMember(m => m.NetoLiquidarTotal, model => model.MapFrom(dto => dto.NetoLiquidar))
                    .ForMember(m => m.TipoCategoria, model => model.MapFrom(dto => dto.TipoCateg));

                cfg.CreateMap<Cheque, ChequeDto>()
                    .ForMember(dto => dto.Documento, model => model.MapFrom(m => m.CuitEmisor))
                    .ForMember(dto => dto.Nombre, model => model.MapFrom(m => m.NombreEmisor))
                    .ForMember(dto => dto.Nosis, model => model.MapFrom(m =>m.EstadoNosisEmisor))
                    .ForMember(dto => dto.TEOps, model => model.MapFrom(m => m.TE))
                    .ForMember(dto => dto.TEAdelantada, model => model.MapFrom(m => m.TEA))
                    .ForMember(dto => dto.Ponderado, model => model.MapFrom(m => m.ImportePonderado));
                
                cfg.CreateMap<ChequeDto, Cheque>()
                    .ForMember(m => m.CuitEmisor, model => model.MapFrom(dto => dto.Documento))
                    .ForMember(m => m.EstadoNosisEmisor, model => model.MapFrom(dto => dto.Nosis))
                    .ForMember(m => m.NombreEmisor, model => model.MapFrom(dto => dto.Nombre))
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

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
                .ForMember(dto => dto.ProvinciaId, model => model.MapFrom(provincia => provincia.Id))
                .ForMember(dto => dto.ProductoId, model => model.MapFrom(producto => producto.Id))
                .ForMember(dto => dto.EmpleadoId, model => model.MapFrom(empleado => empleado.Id))
                .ForMember(dto => dto.Cheques, model => model.MapFrom(s => s.Cheques.ToArray()));
                
                cfg.CreateMap<SimulacionDto,Simulacion>()
                    .ForMember(model => model.Provincia, dto => dto.Ignore())
                    .ForMember(model => model.Producto, dto => dto.Ignore())
                    .ForMember(model => model.Empleado, dto => dto.Ignore())
                    .ForMember(model => model.Cheques, dto => dto.Ignore());
                
                cfg.CreateMap<Cheque, ChequeDto>();
                
                cfg.CreateMap<ChequeDto, Cheque>();

                cfg.CreateMap<Provincia, ProvinciaDto>();

                cfg.CreateMap<ProvinciaDto, Provincia>();

                cfg.CreateMap<Producto, ProductoDto>();

                cfg.CreateMap<ProductoDto, Producto>();
            });
            mapper = new Mapper(config);
        }
       
    }
}

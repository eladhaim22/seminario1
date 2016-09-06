using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Mappers;
using AutoMapper;
using Seminario.Model;
using Seminario.NHibernate;
using Seminario.WebServices.Contracts;

namespace Seminario.MapperProject
{

    public class AutoMapperConfig 
    {
        IUnitOfWork _unitOfWork;
        

        public AutoMapperConfig(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public MapperConfiguration Config()
        {
            var config = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<Simulacion, SimulacionDto>()
                    .ForMember(dto => dto.ValorNominal, model => model.MapFrom(m => m.ImporteTotal))
                    .ForMember(dto => dto.Intereses, model => model.MapFrom(m => m.InteresTotal))
                    .ForMember(dto => dto.ComisionAdministrativa, model => model.MapFrom(m => m.ComisionTotal))
                    .ForMember(dto => dto.Sellado, model => model.MapFrom(m => m.SelladoTotal))
                    .ForMember(dto => dto.Iva, model => model.MapFrom(m => m.IvaTotal))
                    .ForMember(dto => dto.NetoLiquidar, model => model.MapFrom(m => m.NetoLiquidarTotal))
                    .ForMember(dto => dto.TipoCateg, model => model.MapFrom(m => m.TipoCategoria))
                    .ForMember(dto => dto.CodProd, model => model.MapFrom(m => m.Producto.Id))
                    .ForMember(dto => dto.Legajo, model => model.MapFrom(m => m.Empleado.Legajo))
                    .ForMember(dto => dto.IdProvincia, model => model.MapFrom(m => m.Provincia.Id));


                cfg.CreateMap<SimulacionDto, Simulacion>()
                    .ForMember(model => model.Provincia, dto => dto.ResolveUsing(src =>
                    {
                        var temp = new Provincia();
                        temp = _unitOfWork.Repository<Provincia>().GetById(src.IdProvincia);
                        return temp;
                    }))

                    .ForMember(model => model.Empleado, dto => dto.ResolveUsing(src =>
                    {

                        var temp = new Empleado();
                        temp = _unitOfWork.Repository<Empleado>().Where(s => s.Legajo == src.Legajo).FirstOrDefault();
                        return temp;
                    }))

                    .ForMember(model => model.Producto, dto => dto.ResolveUsing(src =>
                    {
                        var temp = new Producto();
                        temp = _unitOfWork.Repository<Producto>().GetById(src.CodProd);
                        return temp;
                    }))
                    .ForMember(m => m.ImporteTotal, model => model.MapFrom(dto => dto.ValorNominal))
                    .ForMember(m => m.InteresTotal, model => model.MapFrom(dto => dto.Intereses))
                    .ForMember(m => m.ComisionTotal, model => model.MapFrom(dto => dto.ComisionAdministrativa))
                    .ForMember(m => m.SelladoTotal, model => model.MapFrom(dto => dto.Sellado))
                    .ForMember(m => m.IvaTotal, model => model.MapFrom(dto => dto.Iva))
                    .ForMember(m => m.NetoLiquidarTotal, model => model.MapFrom(dto => dto.NetoLiquidar))
                    .ForMember(m => m.TipoCategoria, model => model.MapFrom(dto => dto.TipoCateg));

                cfg.CreateMap<Cheque, ChequeDto>()
                    .ForMember(dto => dto.Documento, model => model.MapFrom(m => m.CuitEmisor))
                    .ForMember(dto => dto.Nombre, model => model.MapFrom(m => m.NombreEmisor))
                    .ForMember(dto => dto.Nosis, model => model.MapFrom(m => m.EstadoNosisEmisor))
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
            return config;
        }
    }

    /*public class AutoMapperConfig
    {
        IUnitOfWork _unitOfWork;

        public AutoMapperConfig(IUnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;
        }

        public static IMapper Config()
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
                .ForMember(dto => dto.IdProvincia, model => model.MapFrom(m => m.Provincia.Id));


                cfg.CreateMap<SimulacionDto, Simulacion>()
                    .ForMember(model => model.Provincia, dto => dto.ResolveUsing(src =>
                    {
                        var temp = new Provincia();
                        temp = _unitOfWork.Repository<Provincia>().GetById(src.IdProvincia);
                        return temp;
                    }))

                    .ForMember(model => model.Empleado, dto => dto.ResolveUsing(src =>
                    {
                        
                        var temp = new Empleado();
                        temp = UnitOfWork.Repository<Empleado>().GetById(src.Legajo);
                        return temp;
                    }))
                    .ForMember(model => model.Producto, dto => dto.ResolveUsing(src =>
                     {
                         var temp = new Producto();
                         temp = UnitOfWork.Repository<Producto>().GetById(src.CodProd);
                         return temp;
                     }))
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
            return config.CreateMapper();
        }

    }*/
}

using AutoMapper;
using FormularioDinamico.BindModels;
using FormularioDinamico.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormularioDinamico.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMap()
        {
            Mapper.CreateMap<Categoria, CategoriaBM>();
            Mapper.CreateMap<SubCategoria, SubCategoriaBM>();
            Mapper.CreateMap<Campo, CampoBM>();      
        }
 
    }
}